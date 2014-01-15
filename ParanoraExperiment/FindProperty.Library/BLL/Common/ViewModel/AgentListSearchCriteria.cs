using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    public class AgentListSearchCriteria : CommomSearchCriteria
    {
        public override string ToString()
        {
            string str = base.ToString();
            str += "|AL";
            return str;
        }

        public AgentListSearchCriteria()
        {
            IsInit = false;
        }
    }
}
