using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FindProperty.Lib.Ioc
{
    public class ControllerFactoryCreator
    {
        public IControllerFactory Create()
        {
            return IoCManager.Resolve<IControllerFactory>();
        }
    }
}
