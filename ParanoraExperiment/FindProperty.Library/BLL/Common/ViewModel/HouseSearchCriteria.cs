using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class HouseSearchCriteria : CommomSearchCriteria
    {        
        public string Code { get; set; }
        /// <summary>
        /// Type (1, 2, 3, 22)
        /// </summary>
        public string Type { get; set; }
        public string AgentMobile { get; set; }
        public string AgentCName { get; set; }
        public string ScopeMkt { get; set; }
        public string RefNo { get; set; }
        public string ScopeID { get; set; }
        
        public List<string> TagCommonCodes { get; set; }

        public int? SubWay { get; set; }
        public string SubStation { get; set; }
        public string Tag { get; set; }

        public int? ListStyleId { get; set; }


        public HouseSearchCriteria()
        {
            
        }

        public override string ToString()
        {
            string str = base.ToString();
            
            //str += (string.IsNullOrEmpty(ScopeMkt) ? "" : ScopeMkt) + "|";
            //str += (string.IsNullOrEmpty(ScopeID) ? "" : ScopeID)+"|";
            //str += (string.IsNullOrEmpty(Code) ? "" : Code) + "|";
            //str += (string.IsNullOrEmpty(Type) ? "" : Type) + "|";

            str += (SubWay.HasValue ? "|"+SubWay.Value.ToString() : "");
            str += (string.IsNullOrEmpty(SubStation) ? "" : "|"+SubStation);
            str += (string.IsNullOrEmpty(Tag) ? "" : "|" + Tag);
            str += "|HL";
            return str;
        }
    }
}
