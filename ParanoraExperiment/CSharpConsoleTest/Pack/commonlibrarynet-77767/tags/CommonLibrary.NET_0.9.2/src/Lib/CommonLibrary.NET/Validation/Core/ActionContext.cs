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
using System.Text;

namespace CommonLibrary
{


    /// <summary>
    /// Contextual data for an action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionContext<T>
    {
        protected T _item;
        protected IValidationResults _errors;
        protected IStatusResults _messages;


        /// <summary>
        /// Initialize the action context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="errorList"></param>
        /// <param name="messages"></param>
        public ActionContext(T item, IValidationResults errors, IStatusResults messages)
        {
            _item = item;
            _errors = errors;
            _messages = messages;
        }


        /// <summary>
        /// The item for which some action is being performed.
        /// </summary>
        public T Item
        {
            get { return _item; }
            set { _item = value; }
        }


        /// <summary>
        /// List of errors to populate
        /// </summary>
        public IValidationResults Errors
        {
            get { return _errors; }
        }


        /// <summary>
        /// List of messages to populate.
        /// </summary>
        public IStatusResults Messages
        {
            get { return _messages; }
        }
    }
}
