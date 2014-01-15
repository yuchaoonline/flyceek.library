using FindProperty.Lib.Aop.Provider.PIAB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Aop
{
    public class PolicyInjectionFactory
    {
        private static IInjectionCreator _injectionProvider;

        public static IInjectionCreator Create()
        {
            if (_injectionProvider == null)
            {
                _injectionProvider = new PolicyInjectionCreateor();
            }
            return _injectionProvider;
        }
    }
}
