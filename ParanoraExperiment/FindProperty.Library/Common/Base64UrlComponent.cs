using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common
{
    public static class Base64URLComponent
    {
        // Methods
        public static Dictionary<string, string> DecodeQuery(string Query)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                string str = Encoding.UTF8.GetString(Convert.FromBase64String(Query));
                foreach (string str2 in str.Split("&".ToCharArray()))
                {
                    string[] strArray = str2.Split("=".ToCharArray());
                    dictionary.Add(strArray[0], strArray[1]);
                }
            }
            catch (Exception)
            {
            }
            return dictionary;
        }

        public static string EncodeQuery(Dictionary<string, string> Items)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in Items)
            {
                if ((pair.Value != null) && (pair.Value != ""))
                {
                    if (builder.Length != 0)
                    {
                        builder.Append("&");
                    }
                    builder.AppendFormat("{0}={1}", pair.Key, pair.Value);
                }
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.ToString()));
        }

        public static string EncodeQuery(Dictionary<string, string> OriginalQuery, Dictionary<string, string> Items)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in OriginalQuery)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
            foreach (KeyValuePair<string, string> pair in Items)
            {
                if (dictionary.Keys.Contains<string>(pair.Key))
                {
                    dictionary[pair.Key] = pair.Value;
                }
                else
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if ((pair.Value != null) && (pair.Value != ""))
                {
                    if (builder.Length != 0)
                    {
                        builder.Append("&");
                    }
                    builder.AppendFormat("{0}={1}", pair.Key, pair.Value);
                }
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.ToString()));
        }

        public static string EncodeQuery(Dictionary<string, string> OriginalQuery, string Key, string Value)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in OriginalQuery)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
            if (dictionary.Keys.Contains<string>(Key))
            {
                dictionary[Key] = Value;
            }
            else
            {
                dictionary.Add(Key, Value);
            }
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if ((pair.Value != null) && (pair.Value != ""))
                {
                    if (builder.Length != 0)
                    {
                        builder.Append("&");
                    }
                    builder.AppendFormat("{0}={1}", pair.Key, pair.Value);
                }
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.ToString()));
        }
    }


}
