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
using System.Collections.Specialized;
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
    /// Main API for validating/accepting arguments supplied on the command line.
    /// </summary>
    public class ArgsHelper
    {
        /// <summary>
        /// Parse the arguments and checks for named arguments and non-named arguments.
        /// Applies the arguments to the properties of the argument reciever <paramref name="argReciever"/>.
        /// If there were errors during the parsing, logs what arguments are required.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "DefaultSettings01"</param>
        /// <param name="namedArgPrefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <param name="argumentsStore">Object to store the named arguments.</param>
        /// <returns>True if arguments are valid, false otherwise. argsReciever contains the arguments.</returns>
        public static bool Accept(string[] args, string namedArgPrefix, string namedArgValueSeparator, object argsReciever)
        {
            // --help
            if (ArgsHelper.IsRequestForHelp(args))
            {
                Console.WriteLine(ArgsUsage.Build(argsReciever));
                return false;
            }

            // Parse the command line arguments.
            BoolMessageItem<Args> results = ArgsParser.Parse(args, namedArgPrefix, namedArgValueSeparator, argsReciever);
            if (!results.Success)
            {
                Console.WriteLine("ERROR: Invalid arguments supplied.");
                Console.WriteLine(results.Message);
                Console.WriteLine(ArgsUsage.Build(argsReciever));
                return false;
            }
            return true;
        }


        /// <summary>
        /// Validate and accept the arguments.
        /// Assumes that the 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="prefix">--</param>
        /// <param name="keyValueSeperator">=</param>
        /// <param name="argDefs">The list of argument definitions</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Accept(string[] args, string prefix, string keyValueSeperator,
            int minArgs, List<ArgAttribute> argDefs, List<string> examples)
        {
            // Zero arguments.
            if (argDefs == null || argDefs.Count == 0)
                return new BoolMessageItem<Args>(new Args(), true, string.Empty);

            // Expect arguments.
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments were supplied." + Environment.NewLine);
                ArgsUsage.Show(argDefs, examples);
                return new BoolMessageItem<Args>(new Args(), false, "No arguments were supplied.");
            }

            if (ArgsHelper.IsRequestForHelp(args))
            {
                ArgsUsage.Show(argDefs, examples);
                return new BoolMessageItem<Args>(new Args(), false, "Displaying usage");
            }

            // Try to validate and accept the arguments.
            BoolMessageItem<Args> result = ArgsParser.Parse(args, prefix, keyValueSeperator, argDefs);

            // Failure.
            if (!result.Success)
            {
                Console.WriteLine(Environment.NewLine + "[ERRORS]");
                Console.WriteLine(result.Message + Environment.NewLine);
                Console.WriteLine("Enter --help to display argument usage.");
            }

            return result;
        }


        /// <summary>
        /// Returns true if there is only 1 argument with value:
        /// --h -h --help -help
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool IsRequestForHelp(string[] args)
        {
            // Check for help
            if (args.Length == 1)
            {
                string val = args[0].ToLower();
                if (val == "-h" || val == "-help"
                || val == "--h" || val == "--help"
                || val == "/h" || val == "/help"
                || val == "?" || val == "/?"
                || val == "help")
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Gets a list of all the argument definitions that are applied
        /// (via attributes) on the argument reciever object supplied.
        /// </summary>
        /// <param name="argsReciever">Object containing args attributes.</param>
        /// <returns></returns>
        public static List<ArgAttribute> GetArgsFromReciever(object argsReciever)
        {
            // Get all the properties that have arg attributes.
            List<KeyValuePair<ArgAttribute, PropertyInfo>> args = ReflectionAttributeUtils.GetPropsWithAttributesList<ArgAttribute>(argsReciever);
            List<ArgAttribute> argsList = new List<ArgAttribute>();
            args.ForEach((pair) => argsList.Add(pair.Key));
            return argsList;
        }


        /// <summary>
        /// Get the all argument names / values from the object that recievers the arguments.
        /// </summary>
        /// <param name="argsReciever"></param>
        /// <returns></returns>
        public static IDictionary GetArgValues(object argsReciever)
        {
            var dict = new OrderedDictionary();
            GetArgValues(dict, argsReciever);
            return dict;
        }


        /// <summary>
        /// Get the all argument names / values from the object that recievers the arguments.
        /// </summary>
        /// <param name="argsReciever"></param>
        /// <returns></returns>
        public static void GetArgValues(IDictionary argsValueMap, object argsReciever)
        {
            var args = ReflectionAttributeUtils.GetPropsWithAttributesList<ArgAttribute>(argsReciever);
            args.ForEach(argPair =>
            {
                object val = ReflectionUtils.GetPropertyValueSafely(argsReciever, argPair.Value);
                argsValueMap[argPair.Value.Name] = val;
            });
        }


        /// <summary>
        /// Applies the argument values to the object argument reciever.
        /// </summary>
        /// <param name="parsedArgs"></param>
        /// <param name="argReciever"></param>
        /// <param name="errors"></param>
        public static void CheckAndApplyArgs(Args parsedArgs, object argReciever, IList<string> errors)
        {
            List<KeyValuePair<ArgAttribute, PropertyInfo>> mappings = ReflectionAttributeUtils.GetPropsWithAttributesList<ArgAttribute>(argReciever);
            List<ArgAttribute> argSpecs = new List<ArgAttribute>();
            mappings.ForEach((pair) => argSpecs.Add(pair.Key));

            // Set the supplied argument value on the object that should recieve the value.
            ArgsValidator.Validate(parsedArgs, argSpecs, errors, (argAttr, argVal, ndx) =>
            {
                SetValue(argReciever, mappings[ndx], argVal);
            });
        }


        /// <summary>
        /// Set the argument value from command line on the property of the object
        /// recieving the value.
        /// </summary>
        /// <param name="argReciever"></param>
        /// <param name="val"></param>
        /// <param name="rawArgValue"></param>
        private static void SetValue(object argReciever, KeyValuePair<ArgAttribute, PropertyInfo> val, string rawArgValue)
        {
            ArgAttribute argAttr = val.Key;

            // First interpret.
            string argValue = rawArgValue;
            if (argAttr.Interpret)
            {
                argValue = Substitutor.Substitute(argValue);
            }
            ReflectionUtils.SetProperty(argReciever, val.Value, argValue);
        }
    }
}
