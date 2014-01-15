using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.BLL.Findproperty.Facade;
using FindProperty.Lib.Common;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IoCManager.Initialize(new IoCComponentFactoryBase(ConfigInfo.IoCComponetTypeName));

            HouseSearchCriteria searchCriteria = new FindProperty.Lib.BLL.Common.Facade.HouseSearchCriteria().GetSearchCriteria("",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "");

            int postCount = new Post().SelectPostCount(searchCriteria);

            Console.ReadKey();
        }
    }
}
