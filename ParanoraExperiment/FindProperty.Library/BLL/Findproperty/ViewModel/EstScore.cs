using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
    [Serializable]
    public class EstScore
    {
        public MongoDB.Bson.ObjectId _id { get; set; }
        public string code { get; set; }
        public string distname { get; set; }
        public string estname { get; set; }
        public string estaddress { get; set; }
        public int counter { get; set; }
        public int scounter { get; set; }
        public int rcounter { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double avgrentprice { get; set; }
        public double avgprice { get; set; }
        public double avgunitprice { get; set; }
        public double score { get; set; }
       [MongoDB.Bson.Serialization.Attributes.BsonIgnore]
       public int dis { get; set; }
       public double[] locs { get; set; }
    }
}
