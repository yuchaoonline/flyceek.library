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
using System.Collections;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// Extensions to Non-Generic Dictionary
    /// </summary>
    public static class DictionaryNonGenericExtensions
    {

        #region Public dictionary value conversion methods
        /// <summary>
        /// Get the value associated with the key as a int.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(this IDictionary d, object  key)
        {
            return Convert.ToInt32(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a bool.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(this IDictionary d, object  key)
        {
            return Convert.ToBoolean(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this IDictionary d, object  key)
        {
            return Convert.ToString(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a double.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDouble(this IDictionary d, object  key)
        {
            return Convert.ToDouble(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a datetime.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDictionary d, object  key)
        {
            return Convert.ToDateTime(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a long.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetLong(this IDictionary d, object  key)
        {
            return Convert.ToInt64(d[key]);
        }
        #endregion
    }



    /// <summary>
    /// Extensions to Non-Generic Dictionary
    /// </summary>
    public static class DictionaryStringExtensions
    {

        #region Public dictionary value conversion methods
        /// <summary>
        /// Get the value associated with the key as a int.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToInt32(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a bool.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToBoolean(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToString(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a double.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDouble<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToDouble(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a datetime.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToDateTime(d[key]);
        }


        /// <summary>
        /// Get the value associated with the key as a long.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetLong<TKey>(this IDictionary<TKey, string> d, TKey key)
        {
            return Convert.ToInt64(d[key]);
        }
        #endregion
    }
}
