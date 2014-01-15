using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paranora.Library.Aop.DynamicObj
{
    public class DynamicWrapper : DynamicObject
    {
        private readonly object source;

        public DynamicWrapper(object source)
        {
            this.source = source;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methodInfo = source.GetType().GetMethod(binder.Name);
            if (methodInfo != null)
            {
                Func<object, object[], object> func = (s, a) => methodInfo.Invoke(s, a);

                result = MethodCall(func, source, args);

                return true;
            }

            result = null;

            return false;
        }

        protected virtual object MethodCall(Func<object, object[], object> func, object src, object[] args)
        {
            return func(src, args);
        }
    }

    public class TryCatchDynamicWrapper : DynamicWrapper
    {
        public TryCatchDynamicWrapper(object source) : base(source)
        { }

        protected override object MethodCall(Func<object, object[], object> func, object src, object[] args)
        {
            try
            {
                return base.MethodCall(func, src, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
