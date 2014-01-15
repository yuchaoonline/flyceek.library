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
    /// Class providing utility methods for parsing a string or collection of arguments.
    /// </summary>
    public class ArgsParser
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
        /// <param name="namedArgPrefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string text, string namedArgPrefix, string namedArgValueSeparator)
        {
            return TryCatch(() => Parse(LexArgs.ParseList(text).ToArray(), namedArgPrefix, namedArgValueSeparator));
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="namedArgPrefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string text, string namedArgPrefix, string namedArgValueSeparator, object argReciever)
        {
            return TryCatch(() => Parse(LexArgs.ParseList(text).ToArray(), namedArgPrefix, namedArgValueSeparator, argReciever));
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
        /// <param name="namedArgPrefix">Character used to prefix the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string namedArgPrefix, string namedArgValueSeparator)
        {
            // Validate the inputs.
            BoolMessageItem<Args> validationResult = ArgsValidator.ValidateInputs(args, namedArgPrefix, namedArgValueSeparator);
            if (!validationResult.Success) return validationResult;

            bool checkNamedArgs = !string.IsNullOrEmpty(namedArgValueSeparator);
            bool hasPrefix = !string.IsNullOrEmpty(namedArgPrefix);

            // Name of argument can only be letter, number, (-,_,.).
            // The value can be anything.
            string regexPattern = @"(?<name>[a-zA-Z0-9\-_\.]+)" + namedArgValueSeparator + @"(?<value>.+)";
            regexPattern = hasPrefix ? namedArgPrefix + regexPattern : regexPattern;

            Args resultArgs = new Args();
            List<string> errors = new List<string>();

            // Put the arguments into the named args dictionary and/or list.
            if (checkNamedArgs)
            {
                ParseArgs(args, resultArgs.NamedArgs, resultArgs.ArgsList, regexPattern);
            }
            else
            {
                resultArgs.ArgsList = new List<string>(args);
            }
            return new BoolMessageItem<Args>(resultArgs, true, string.Empty);
        }


        /// <summary>
        /// Parses the arguments and checks for named arguments and non-named arguments.
        /// </summary>
        /// <param name="args">e.g. "-config:prod.xml", "-date:T-1", "BloombergFutOpt"</param>
        /// <param name="namedArgPrefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <param name="argumentsStore">Object to store the named arguments.</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string namedArgPrefix, string namedArgValueSeparator, object argReciever)
        {
            // Parse the args first.
            BoolMessageItem<Args> parseResult = Parse(args, namedArgPrefix, namedArgValueSeparator);
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
        /// <param name="namedArgPrefix">Character used to identifiy the name 
        /// of a named parameter. e.g. "-" as in "-config:prod.xml"
        /// Leave null or string.emtpy to indicate there is no prefix.
        /// In which case, only the <paramref name="namedArgValueSeparator"/> is used to distinguish 
        /// namevalue pairs.</param>
        /// <param name="namedArgValueSeparator">Character used to separate the named 
        /// argument with it's value in a named argument. e.g. ":" as in "-config:prod.xml"
        /// If this is null or string.empty, in addition to the <paramref name="namedArgPrefix"/></param>
        /// <param name="argumentsStore">Object to store the named arguments.</param>
        /// <returns></returns>
        public static BoolMessageItem<Args> Parse(string[] args, string namedArgPrefix, string namedArgValueSeparator, List<ArgAttribute> argsSpec)
        {
            // Parse the args first.
            BoolMessageItem<Args> parseResult = Parse(args, namedArgPrefix, namedArgValueSeparator);
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
        /// <param name="args"></param>
        /// <param name="namedArgs"></param>
        /// <param name="unnamedArgs"></param>
        /// <param name="regexPattern"></param>
        public static void ParseArgs(string[] args, IDictionary<string, string> namedArgs, List<string> unnamedArgs, string regexPattern)
        {
            foreach (string arg in args)
            {
                Match match = Regex.Match(arg, regexPattern);
                if (match != null && match.Success)
                {
                    string name = match.Groups["name"].Value;
                    string val = match.Groups["value"].Value;
                    namedArgs[name] = val;
                }
                else
                {
                    unnamedArgs.Add(arg);
                }
            }
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
