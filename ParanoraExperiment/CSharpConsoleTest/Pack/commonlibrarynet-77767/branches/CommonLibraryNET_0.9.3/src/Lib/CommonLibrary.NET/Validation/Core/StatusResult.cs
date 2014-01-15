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


namespace ComLib.Validation
{

    /// <summary>
    /// A single Validation result
    /// </summary>
    public class StatusResult : IStatusResult
    {
        private string _key;        
        private string _message;
        private object _target;


        /// <summary>
        /// Initalized all the read-only
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <param name="isValid"></param>
        public StatusResult(string key, string message, object target)
        {
            SetInfo(key, message, target);
        }


        /// <summary>
        /// Initalize
        /// </summary>
        /// <param name="error"></param>
        /// <param name="isValid"></param>
        public StatusResult(string message, object target)
        {
            SetInfo(null, message, target);
        }


        /// <summary>
        /// Set the info.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <param name="isValid"></param>
        public void SetInfo(string key, string message, object target)
        {
            _key = key;
            _message = message;
            _target = target;
        }


        /// <summary>
        /// Message or error
        /// </summary>
        public string Message
        {
            get { return _message; }
        }


        /// <summary>
        /// Key which can represent a specific field/property to serve
        /// as contextual information for an action result.
        /// </summary>
        public string Key
        {
            get { return _key; }
        }


        /// <summary>
        /// The object associated with this action.
        /// </summary>
        public object Target
        {
            get { return _target; }
        }


        /// <summary>
        /// String representation of action result.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            bool hasKey = (_key != null && _key != "");

            if (hasKey)
                result = _key + " : " + _message;
            else
                result = _message;

            return result;
        }
    }
}