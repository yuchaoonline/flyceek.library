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



namespace GenericCode
{
   
    public class StringHelpers
    {
        /// <summary>
        /// Get delimited chars from a string.
        /// </summary>
        /// <param name="rawText">search-classes-workshops-4-1-1-6</param>
        /// <param name="excludeText">search-classes-workshops</param>
        /// <param name="delimiter">-</param>
        /// <returns></returns>
        public static string[] GetDelimitedChars(string rawText, string excludeText, char delimiter)
        {
            int indexOfDelimitedData = rawText.IndexOf(excludeText);
            string delimitedData = rawText.Substring(indexOfDelimitedData + excludeText.Length);
            string[] separatedChars = delimitedData.Split(delimiter);
            return separatedChars;
        }


        /// <summary>
        /// Truncates the string.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string Truncate(string txt, int maxChars)
        {
            if(string.IsNullOrEmpty(txt) )
                return txt;
            
            if(txt.Length <= maxChars)
                return txt;
            
            return txt.Substring(0, maxChars);
        }


        /// <summary>
        /// Truncate with text
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="maxChars"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string TruncateWithText(string txt, int maxChars, string suffix)
        {
            if (string.IsNullOrEmpty(txt))
                return txt;

            if (txt.Length <= maxChars)
                return txt;

            // Now do the truncate and more.
            string partial = txt.Substring(0, maxChars);
            return partial + suffix;
        }


        /// <summary>
        /// If null returns empty string.
        /// Else, returns original.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetOriginalOrEmptyString(string text)
        {
            if (text == null) { return string.Empty; }
            return text;
        }


        /// <summary>
        /// Returns the defaultval if the val string is null or empty.
        /// Returns the val string otherwise.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string GetDefaultStringIfEmpty(string val, string defaultVal)
        {
            if (string.IsNullOrEmpty(val)) return defaultVal;

            return val;
        }        


        /// <summary>
        /// Convert the word(s) in the sentence to sentence case.
        /// UPPER = Upper
        /// lower = Lower
        /// MiXEd = Mixed
        /// </summary>
        /// <param name="s"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ConvertToSentanceCase(string s, char delimiter)
        {
            // Check null/empty
            if (string.IsNullOrEmpty(s))
                return s;

            s = s.Trim();
            if (string.IsNullOrEmpty(s))
                return s;

            // Only 1 token
            if (s.IndexOf(delimiter) < 0)
            {
                s = s.ToLower();
                s = s[0].ToString().ToUpper() + s.Substring(1);
                return s;
            }

            // More than 1 token.
            string[] tokens = s.Split(delimiter);
            StringBuilder buffer = new StringBuilder();

            foreach (string token in tokens)
            {
                string currentToken = token.ToLower();
                currentToken = currentToken[0].ToString().ToUpper() + currentToken.Substring(1);
                buffer.Append(currentToken + delimiter);
            }

            s = buffer.ToString();
            return s.TrimEnd(delimiter);
        }


        /// <summary>
        /// Get the index of a spacer ( space" " or newline )
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        public static int GetIndexOfSpacer(string txt, int currentPosition, ref bool isNewLine)
        {
            // Take the first spacer that you find. it could be eithr
            // space or newline, if space is before the newline take space
            // otherwise newline.            
            int ndxSpace = txt.IndexOf(" ", currentPosition);
            int ndxNewLine = txt.IndexOf(Environment.NewLine, currentPosition);
            bool hasSpace = ndxSpace > -1;
            bool hasNewLine = ndxNewLine > -1;
            isNewLine = false;

            // Found both space and newline.
            if (hasSpace && hasNewLine)
            {
                if (ndxSpace < ndxNewLine) { return ndxSpace; }
                isNewLine = true;
                return ndxNewLine;
            }

            // Found space only.
            if (hasSpace && !hasNewLine) { return ndxSpace; }

            // Found newline only.
            if (!hasSpace && hasNewLine) { isNewLine = true; return ndxNewLine; }

            // no space or newline.
            return -1;
        }


        /// <summary>
        /// Convert boolean value to "Yes" or "No"
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ConvertBoolToYesNo(bool b)
        {
            if (b) { return "Yes"; }

            return "No";
        }
    }

}
