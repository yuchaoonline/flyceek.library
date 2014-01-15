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
using ComLib.Parsing;

namespace ComLib.Arguments
{
    /// <summary>
    /// Class providing utility methods for parsing a string or collection of arguments.
    /// </summary>
    public partial class Args
    {
        /// <summary>
        /// Parse the line into a collection of arguments.
        /// </summary>
        /// <param name="text">"-config:prod.xml -date:T-1 BloombergFutOpt"</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string text)
        {
            return TryCatch(() => Parse(LexArgs.ParseList(text).ToArray()));
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="prefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string text, string prefix, string separator)
        {
            return TryCatch(() => Parse(LexArgs.ParseList(text).ToArray(), prefix, separator));
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="prefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string text, string prefix, string separator, object argReciever)
        {
            return TryCatch(() => Parse(LexArgs.ParseList(text).ToArray(), prefix, separator, argReciever));
        }


        /// <summary>
        /// Parse the arguments into a collection of namedArguments and
        /// unnamed arguments using the default strings to identify named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <example>Given : "-config:prod.xml", "-date:T-1", "BloombergFutOpt"
        /// The items -config and -date are named arguments by default, 
        /// and BloombergFutOpt is just a regular argument.
        /// </example>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args)
        {
            return Parse(args, "-", ":");
        }


        /// <summary>
        /// Parses the arguments
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="prefix">Character used to prefix the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string prefix, string separator)
        {
            // Validate the inputs.
            BoolMessageItem<Args> validationResult = ArgsValidator.ValidateInputs(args, prefix, separator);
            if (!validationResult.Success) return validationResult;

            bool checkNamedArgs = !string.IsNullOrEmpty(separator);
            bool hasPrefix = !string.IsNullOrEmpty(prefix);

            // Name of argument can only be letter, number, (-,_,.).
            // The value can be anything.
            string patternKeyValue = @"(?<name>[a-zA-Z0-9\-_\.]+)" + separator + @"(?<value>.+)";
            string patternBool = @"(?<name>[a-zA-Z0-9\-_\.]+)";
            patternKeyValue = hasPrefix ? prefix + patternKeyValue : patternKeyValue;
            patternBool = hasPrefix ? prefix + patternBool : patternBool;

            Args resultArgs = new Args(args, prefix, separator);
            List<string> errors = new List<string>();

            // Put the arguments into the named args dictionary and/or list.
            if (checkNamedArgs)
            {
                Parse(args, resultArgs.Named, resultArgs.Positional, patternKeyValue, patternBool);
            }
            else
            {
                resultArgs.Positional = new List<string>(args);
            }
            return new BoolMessageItem<Args>(resultArgs, true, string.Empty);
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="prefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <param name="argumentsStore">Object to store the named arguments.</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string prefix, string separator, object argReciever)
        {
            // Parse the args first.
            BoolMessageItem<Args> parseResult = Parse(args, prefix, separator);
            if (!parseResult.Success) return parseResult;

            Args resultArgs = parseResult.Item;
            List<string> errors = new List<string>();

            // Set the named argument values on the object's properties.
            if (argReciever != null)
            {
                ArgsHelper.CheckAndApplyArgs(resultArgs, argReciever, errors);
            }
            string singleMessage = string.Empty;
            foreach (string error in errors) { singleMessage += error + Environment.NewLine; }

            return new BoolMessageItem<Args>(resultArgs, errors.Count == 0, singleMessage);
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="argsSpec">List of expected argument definitions(both named and positional).</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, List<ArgAttribute> argsSpec)
        {
            return Parse(args, "-", ":", argsSpec);
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="prefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <param name="argsSpec">List of expected argument definitions(both named and positional).</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string prefix, string separator, List<ArgAttribute> argsSpec)
        {
            // Parse the args first.
            BoolMessageItem<Args> parseResult = Parse(args, prefix, separator);
            if (!parseResult.Success) return parseResult;

            Args resultArgs = parseResult.Item;
            List<string> errors = new List<string>();

            // Set the named argument values on the object's properties.
            if (argsSpec != null && argsSpec.Count > 0)
            {
                ArgsValidator.Validate(resultArgs, argsSpec, errors, null);
            }
            string singleMessage = string.Empty;
            foreach (string error in errors) { singleMessage += error + Environment.NewLine; }

            return new BoolMessageItem<Args>(resultArgs, errors.Count == 0, singleMessage);
        }


        /// <summary>
        /// Checks for named args and gets the name and corresponding value.
        /// </summary>
        /// <param name="args">The arguments to parse</param>
        /// <param name="namedArgs">Dictionary to populate w/ named arguments.</param>
        /// <param name="unnamedArgs">List to populate with unamed arguments.</param>
        /// <param name="regexPatternWithValue">Regex pattern for key/value pair args.
        /// e.g. -env:prod where key=env value=prod</param>
        /// <param name="regexPatternBool">Regex pattern for boolean based key args.
        /// -sendemail key=sendemail the value is automatically set to true.
        /// This is useful for enabled options e.g. -sendemail -recurse </param>
        public static void Parse(string[] args, IDictionary<string, string> namedArgs, List<string> unnamedArgs, 
            string regexPatternWithValue, string regexPatternBool)
        {
            foreach (string arg in args)
            {
                Match matchKeyValue = Regex.Match(arg, regexPatternWithValue);
                Match matchBool = Regex.Match(arg, regexPatternBool);

                if (matchKeyValue != null && matchKeyValue.Success)
                {
                    string name = matchKeyValue.Groups["name"].Value;
                    string val = matchKeyValue.Groups["value"].Value;
                    namedArgs[name] = val;
                }
                else if(matchBool != null && matchBool.Success)
                {
                    string name = matchBool.Groups["name"].Value;
                    namedArgs[name] = "true";
                }
                else
                {
                    unnamedArgs.Add(arg);
                }
            }
        }


        /// <summary>
        /// Parse the arguments and checks for named arguments and non-named arguments.
        /// Applies the arguments to the properties of the argument reciever <paramref name="argReciever"/>.
        /// If there were errors during the parsing, logs what arguments are required.
        /// </summary>
        /// <param name="rawArgs">e.g. "-config:prod.xml", "-date:T-1", "DefaultSettings01"</param>
        /// <param name="prefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="separator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="separator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="prefix"/></param>
        /// <param name="argumentsStore">Object to store the named arguments.</param>
        /// <returns>True if arguments are valid, false otherwise. argsReciever contains the arguments.</returns>
        public static bool Accept(string[] rawArgs, string prefix, string separator, object argsReciever)
        {
            Args args = new Args(rawArgs, prefix, separator);
            if (args.IsHelp)
            {
                Console.WriteLine(ArgsUsage.Build(argsReciever));
                return false;
            }

            // Parse the command line arguments.
            BoolMessageItem<Args> results = Args.Parse(rawArgs, prefix, separator, argsReciever);
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
        public static BoolMessageItem<Args> Accept(string[] rawArgs, string prefix, string keyValueSeperator,
            int minArgs, List<ArgAttribute> supported, List<string> examples)
        {
            Args args = new Args(rawArgs, supported);
            if( args.IsEmpty )
            {
                Console.WriteLine("No arguments were supplied." + Environment.NewLine);
                ArgsUsage.Show(supported, examples);
                return new BoolMessageItem<Args>(args, false, "No arguments were supplied.");
            }

            // Try to validate and accept the arguments.
            BoolMessageItem<Args> result = Args.Parse(rawArgs, prefix, keyValueSeperator, supported);
            return result;
        }


        #region Private members
        /// <summary>
        /// Wrap the parsing of the arguments into a trycatch.
        /// </summary>
        /// <param name="executor"></param>
        /// <returns></returns>
        private static BoolMessageItem<Args> TryCatch(Func<BoolMessageItem<Args>> executor)
        {
            try
            {
                return executor();
            }
            catch (Exception ex)
            {
                return new BoolMessageItem<Args>(null, false, ex.Message);
            }
        }
        #endregion
    }   
}
