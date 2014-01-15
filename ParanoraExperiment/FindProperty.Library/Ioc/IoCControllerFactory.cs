using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace FindProperty.Lib.Ioc
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        public IoCControllerFactory()
        {

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            if (controllerType == null)
            {
                throw new ArgumentNullException("controllerType");
            }

            IController controller = IoCManager.Resolve<IController>(controllerType);

            return controller;
        }
    }
}
