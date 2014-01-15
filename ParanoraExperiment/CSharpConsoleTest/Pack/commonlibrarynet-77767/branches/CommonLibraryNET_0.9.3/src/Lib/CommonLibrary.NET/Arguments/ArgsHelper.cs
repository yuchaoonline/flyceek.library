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

using ComLib;
using ComLib.Subs;
using ComLib.Reflection;

namespace ComLib.Arguments
{
    /// <summary>
    /// Main API for validating/accepting arguments supplied on the command line.
    /// </summary>
    public class ArgsHelper
    {

        /// <summary>
        /// Gets a list of all the argument definitions that are applied
        /// (via attributes) on the argument reciever object supplied.
        /// </summary>
        /// <param name="argsReciever">Object containing args attributes.</param>
        /// <returns></returns>
        public static List<ArgAttribute> GetArgsFromReciever(object argsReciever)
        {
            // Get all the properties that have arg attributes.
            List<KeyValuePair<ArgAttribute, PropertyInfo>> args = AttributeHelper.GetPropsWithAttributesList<ArgAttribute>(argsReciever);
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
            var args = AttributeHelper.GetPropsWithAttributesList<ArgAttribute>(argsReciever);
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
            List<KeyValuePair<ArgAttribute, PropertyInfo>> mappings = AttributeHelper.GetPropsWithAttributesList<ArgAttribute>(argReciever);
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
