using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    public class AgentInfoSearchCriteria : CommomSearchCriteria
    {
        public string CestCode { get; set; }
        public string EstName { get; set; }
        public string AgentName { get; set; }
        public string AgentNo { get; set; }

        public override string ToString()
        {
            string str = base.ToString();

            str += (string.IsNullOrEmpty(AgentName) ? "" : "|" + AgentName);
            str += (string.IsNullOrEmpty(CestCode) ? "" : "|" + CestCode);
            str += (string.IsNullOrEmpty(AgentNo) ? "" : "|" + AgentNo);
            str += "|AI";

            return str;
        }
    }
}
