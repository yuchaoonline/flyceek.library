using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
    public class Branch
    {
        public MongoDB.Bson.ObjectId _id { get; set; }
        public string branchname { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string address { get; set; }
        public string addressdetail { get; set; }
        public string scp_id { get; set; }
        public string scp_name { get; set; }
        public int orgcount { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnore]
        public int dis { get; set; }
        public double[] locs { get; set; }
    }
}
