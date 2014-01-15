using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SH = FindProperty.Lib.BLL.SHTagToSalesBlog;
using FP = FindProperty.Lib.BLL.Findproperty;

namespace FindProperty.Lib.BLL.Common.DAL.MDb
{
    public class MatchHouseSearchKeyword:IDAL.IMatchHouseSearchKeyword
    {
        public ViewModel.MatchHouseSearchKeyword Match(string keyWork, string ip, string session)
        {
            ViewModel.MatchHouseSearchKeyword mt = new ViewModel.MatchHouseSearchKeyword();

            List<SH.ViewModel.TagArea> tagAreas = new SH.Facade.TagArea().SelectTagAreaByDistname(keyWork);

            if (tagAreas != null && tagAreas.Count() > 0)
            {
                mt.ScopeMkt = tagAreas[0].SCP_MKT;
            }
            else
            {
                List<SH.ViewModel.TagAgent> tagAgents = new List<SH.ViewModel.TagAgent>();
                if (new Regex(@"^\d+$").Match(keyWork).Success)
                {
                    tagAgents = new SH.Facade.TagAgent().SelectTagAgent("", "", keyWork, 1);
                    if (tagAgents != null && tagAgents.Count() > 0)
                    {
                        mt.AgentMobile = tagAgents[0].AgentMobile;
                        mt.AgentNo = tagAgents[0].AgentNo;
                        mt.AgentCName = tagAgents[0].AgentCName;
                    }
                }
                else
                {
                    tagAgents = new SH.Facade.TagAgent().SelectTagAgent("", keyWork, "", 1);
                    if (tagAgents != null && tagAgents.Count() > 0)
                    {
                        mt.AgentCName = tagAgents[0].AgentCName;
                        mt.AgentNo = tagAgents[0].AgentNo;
                    }
                }
                if (tagAgents == null && tagAgents.Count < 1)
                {
                    ;
                }
            }

            return mt;
        }
    }
}
