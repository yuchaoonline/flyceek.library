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

using ComLib;


namespace ComLib.ValidationSupport
{
    /// <summary>
    /// Sql injection rule to prevent sql keywords being used in the
    /// search.
    /// </summary>
    public class SearchSqlInjectionRule : ValidatorBase
    {
        private string[] _sentences;
        private static IList<string> _sqlInjectionWords;

        public const string SqlInjection_Delete = "delete";
        public const string SqlInjection_Update = "update";
        public const string SqlInjection_Insert = "insert";
        public const string SqlInjection_Drop = "drop";
        public const string SqlInjection_Select = "select";
        public const string SqlInjection_Exec = "exec ";
        public const string SqlInjection_Execute = "execute ";
        public const string SqlInjection_Grant = "grant";


        static SearchSqlInjectionRule()
        {
            _sqlInjectionWords = new List<string>();
            _sqlInjectionWords.Add(SqlInjection_Delete);
            _sqlInjectionWords.Add(SqlInjection_Insert);
            _sqlInjectionWords.Add(SqlInjection_Drop);
            _sqlInjectionWords.Add(SqlInjection_Update);
            _sqlInjectionWords.Add(SqlInjection_Select);
            _sqlInjectionWords.Add(SqlInjection_Exec);
            _sqlInjectionWords.Add(SqlInjection_Execute);
            _sqlInjectionWords.Add(SqlInjection_Grant);
        }


        /// <summary>
        /// Sql Injection rule to prevent sql keywords to be put into the 
        /// keywords or location.
        /// 
        /// **** NOTE:
        /// 
        /// THIS IS A TEMPORARY workaround as the stored procedure needs
        /// to be modified using sp_executeSql instead of exec.
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="location"></param>
        public SearchSqlInjectionRule(string[] sentences)
        {
            _sentences = sentences;
        }


        /// <summary>
        /// Validate the rule against the data.
        /// </summary>        
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results;
            bool useTarget = validationEvent.Target != null;
            
            string[] sentences = useTarget ? (string[])target : _sentences;
            int initialErrorCount = results.Count;

            foreach (string sentence in sentences)
            {
                if (!string.IsNullOrEmpty(sentence))
                {
                    string lowerKeyWords = sentence.ToLower();
                    if (ContainsAnySqlWords(sentence))
                    {
                        results.Add("Invalid search criteria in keywords.");
                    }
                }
            }
            return initialErrorCount == results.Count;        
        }


        private bool ContainsAnySqlWords(string lowerCaseText)
        {
            int length = _sqlInjectionWords.Count;
            for (int ndx = 0; ndx < length; ndx++)
            {
                if (lowerCaseText.IndexOf(_sqlInjectionWords[ndx]) >= 0)
                    return true;
            }
            return false;
        }
    }
}
