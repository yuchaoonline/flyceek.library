using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL
{
    public interface ITagAgent
    {
        [CommonCallHandler(CacheSecond = 30 * 60)]
        List<ViewModel.TagAgent> SelectTagAgent(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order);

        [CommonCallHandler(CacheSecond = 30 * 60)]
        List<ViewModel.TagAgent> SelectTagAgentLike(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order);
    }
}
