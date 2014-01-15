using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class MatchHouseSearchKeyword
    {
        public string AgentMobile { get; set; }

        public string AgentCName { get; set; }

        public string ScopeMkt { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public List<string> TagCommonCodes { get; set; }

        public string KeyWord { get; set; }

        public string RefNo { get; set; }

        public string AgentNo { get; set; }

        public string AgentName { get; set; }

    }
}
