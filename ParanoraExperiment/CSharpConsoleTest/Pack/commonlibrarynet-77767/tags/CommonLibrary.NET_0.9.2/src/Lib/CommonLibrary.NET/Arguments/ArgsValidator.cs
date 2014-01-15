/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;


namespace CommonLibrary
{
    /// <summary>
    /// Validator class for arguments/commandline options given the
    /// argument specifications.
    /// </summary>
    public class ArgsValidator
    {
        /// <summary>
        /// Validate the inputs for parsing the arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="namedArgPrefix"></param>
        /// <param name="namedArgValueSeparator"></param>
        /// <param name="argSpecs"></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> ValidateInputs(string[] args, string namedArgPrefix, string namedArgValueSeparator)
        {
            // Validate.
            if (!string.IsNullOrEmpty(namedArgPrefix) && string.IsNullOrEmpty(namedArgValueSeparator))
                return new BoolMessageItem<Args>(null, false, "Must provide a name/value separator if providing a prefix for named arguments.");

            if (args == null || args.Length == 0)
                return new BoolMessageItem<Args>(null, false, "There are 0 arguments to parse.");

            return new BoolMessageItem<Args>(null, true, string.Empty);
        }


        /// <summary>
        /// Validate the parsed args supplied with the args specification list.
        /// </summary>
        /// <param name="parsedArgs"></param>
        /// <param name="argSpecs"></param>
        /// <param name="errors"></param>
        public static void Validate(Args parsedArgs, List<ArgAttribute> argSpecs, IList<string> errors,
            Action<ArgAttribute, string, int> onArgumentValidationSuccessCallback)
        {
            // Get all the properties that have arg attributes.            
            bool hasPositionalArgs = parsedArgs.ArgsList != null && parsedArgs.ArgsList.Count > 0;
            int positionalArgCount = hasPositionalArgs ? parsedArgs.ArgsList.Count : 0;

            // Go through all the arg specs.
            for (int ndx = 0; ndx < argSpecs.Count; ndx++)
            {
                ArgAttribute argAttr = argSpecs[ndx];
                string argVal = string.Empty;
                int initialErrorCount = errors.Count;

                // Named argument. key=value
                if (!string.IsNullOrEmpty(argAttr.Name))
                {
                    argVal = parsedArgs.NamedArgs.ContainsKey(argAttr.Name) ? parsedArgs.NamedArgs[argAttr.Name] : string.Empty;
                    ValidateArg(argAttr, argVal, errors);
                }
                else
                {
                    // Index argument [0] [1]
                    bool validIndex = ValidateIndex(argAttr, positionalArgCount, errors);
                    if (validIndex)
                    {
                        argVal = parsedArgs.ArgsList[argAttr.IndexPosition];
                        ValidateArg(argAttr, argVal, errors);
                    }
                }

                // Notify if successful validation of single attribute.
                if (initialErrorCount == errors.Count && onArgumentValidationSuccessCallback != null)
                    onArgumentValidationSuccessCallback(argAttr, argVal, ndx);
            }
        }


        /// <summary>
        /// Validates various aspects of the argument.
        /// </summary>
        /// <param name="argAttr"></param>
        /// <param name="argVal"></param>
        /// <param name="errors"></param>
        public static bool ValidateArg(ArgAttribute argAttr, string argVal, IList<string> errors)
        {
            // Arg name or index.
            string argId = string.IsNullOrEmpty(argAttr.Name) ? "[" + argAttr.IndexPosition + "]" : argAttr.Name;
            int initialErrorCount = errors.Count;

            // Argument missing and required.
            if (argAttr.IsRequired && string.IsNullOrEmpty(argVal))
            {
                errors.Add(string.Format("Required argument '{0}' : {1} is missing.", argAttr.Name, argAttr.DataType.FullName));
                return false;
            }

            // Argument missing and Optional - Can't do much.
            if (!argAttr.IsRequired && string.IsNullOrEmpty(argVal))
                return true;

            // File doesn't exist.
            if (argAttr.DataType == typeof(System.IO.File) && !System.IO.File.Exists(argVal))
                errors.Add(string.Format("File '{0}' associated with argument '{1}' does not exist.", argVal, argId));

            // Wrong data type.
            else if (!argAttr.Interpret && !ReflectionTypeChecker.CanConvertTo(argAttr.DataType, argVal))
                errors.Add(string.Format("Argument value of '{0}' for '{1}' does not match type {2}.",
                           argVal, argId, argAttr.DataType.FullName));

            return initialErrorCount == errors.Count;
        }


        /// <summary>
        /// Validates the index position of the non-named argument.
        /// </summary>
        /// <param name="argAttr"></param>
        /// <param name="positionalArgCount"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool ValidateIndex(ArgAttribute argAttr, int positionalArgCount, IList<string> errors)
        {
            // Now check the positional args.
            bool isValidIndex = argAttr.IndexPosition < positionalArgCount;


            // Required and positional arg valid.
            if (argAttr.IsRequired && !isValidIndex)
            {
                errors.Add(string.Format("Un-Named argument at index : [{0}]' was not supplied.", argAttr.IndexPosition));
            }
            return isValidIndex;
        }
    }    
}
