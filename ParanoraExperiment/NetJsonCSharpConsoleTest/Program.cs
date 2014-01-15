using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetJsonTest
{
    class Program
    {
       

        public static string Request(string url, string parameters, string datatype = "json", string httpMethod = "GET", string ContentType = "application/x-www-form-urlencoded")
        {
            if (httpMethod.ToUpper() == "GET")
            {
                url += "?" + parameters;
            }
            

            string json = string.Empty;
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = httpMethod;
            if (datatype == "json")
                webRequest.Accept = "application/json";
            else
            {
                webRequest.Accept = "text/html";
            }

            webRequest.ContentType = ContentType;

            if (httpMethod.ToUpper() == "POST")
            {
                string query = parameters;
                byte[] mybyte = System.Text.UTF8Encoding.UTF8.GetBytes(query);
                webRequest.ContentLength = mybyte.Length;
                Stream stream = webRequest.GetRequestStream();
                if (stream.CanWrite)
                {
                    stream.Write(mybyte, 0, mybyte.Length);
                }

            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webRequest.GetResponse()
                        .GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                json = "err:" + ex.Message;
            }
            return json;
            
        }
   
        static string ToJson(object value)
        {
            Type type = value.GetType();
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);
            writer.Formatting = Formatting.None;
            //writer.QuoteChar = '"';
            json.Serialize(writer, value);
            string output = sw.ToString();
            writer.Close();
            sw.Close();
            return output;
        }

        static T FromJson<T>(string jsonText)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            StringReader sr = new StringReader(jsonText);
            Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);
            T result = (T)json.Deserialize(reader, typeof(T));
            reader.Close();
            return result;
        }

        static void Main(string[] args)
        {
            string str = Request("http://10.28.6.40:8899/api/CriteriaState", "");



            var obj = FromJson<CriteriaState>(str);

            
            Console.ReadKey();
        }
    }
    public class CriteriaState
    {
        public float RegionVersion { get; set; }
        public float GscpVersion { get; set; }
        public float PriceVersion { get; set; }
        public float BedRoomVersion { get; set; }
        public float SubWayVersion { get; set; }
        public float SubStationVersion { get; set; }
        public float SearchRecommendTag { get; set; }
    }

    public class Post
    {
        [JsonProperty(PropertyName = "TIT")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "RF")]
        public string RefNo { get; set; }

        [JsonProperty(PropertyName = "CEST")]
        public string CnEstate { get; set; }

        [JsonProperty(PropertyName = "BRC")]
        public int BedroomCount { get; set; }

        [JsonProperty(PropertyName = "SRC")]
        public int SittingRoomCount { get; set; }

        [JsonProperty(PropertyName = "S")]
        public decimal Size { get; set; }

        [JsonProperty(PropertyName = "SPCN")]
        public string scpCName { get; set; }

        //[JsonProperty(PropertyName = "SMKT")]
        //public string scpMktc { get; set; }

        [JsonProperty(PropertyName = "GPCN")]
        public string gscpCName { get; set; }

        [JsonProperty(PropertyName = "TP")]
        public string ThumbPath { get; set; }

        [JsonProperty(PropertyName = "P")]
        public decimal? Price { get; set; }
        [JsonProperty(PropertyName = "R")]
        public decimal? Rental { get; set; }

        [JsonProperty(PropertyName = "PT")]
        public string PostType { get; set; }
    }

    public class BaseAgent
    {
        public String AgentCName { get; set; }

        //public String AgentName { get; set; }

        public String AgentNo { get; set; }

        public String AgentMobile { get; set; }

        public String AgentEmail { get; set; }

        public String BranchCName { get; set; }

        //public string ManagerNo { get; set; }

        public int post_counter { get; set; }

        public decimal? agentscore { get; set; }

        public List<Post> Posts { get; set; }
    }

    [Serializable]
    public partial class TagSearchMkt
    {
        public TagSearchMkt()
        {
            TagCommonCode = Guid.Empty;
            _tagcount = null;
            _seq = null;
        }
        #region Model
        private string _scpmkt;
        private Guid _tagcommoncode;
        private string _tag;
        private string _tagcategory;
        private decimal? _tagcount;
        private long? _seq;
        /// <summary>
        /// 
        /// </summary>
        public string scpMkt
        {
            set { _scpmkt = value; }
            get { return _scpmkt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid TagCommonCode
        {
            set { _tagcommoncode = value; }
            get { return _tagcommoncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag
        {
            set { _tag = value; }
            get { return _tag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TagCategory
        {
            set { _tagcategory = value; }
            get { return _tagcategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TagCount
        {
            set { _tagcount = value; }
            get { return _tagcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? seq
        {
            set { _seq = value; }
            get { return _seq; }
        }
        #endregion Model

    }
}
