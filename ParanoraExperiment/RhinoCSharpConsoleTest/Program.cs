using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhino
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Fun1()
        {
            var cx = org.mozilla.javascript.Context.enter();
            try
            {
                var scope = cx.initStandardObjects();
                cx.evaluateString(scope, @"var checkName = function(name) { return /^\w{3,10}$/.test(name); }", "checkName.js", 1, null);
                var func = (org.mozilla.javascript.Function)scope.get("checkName", scope);

                Console.WriteLine(org.mozilla.javascript.Context.toString(func.call(cx, scope, scope, new object[] { "jeffz" })));
                Console.WriteLine(org.mozilla.javascript.Context.toString(func.call(cx, scope, scope, new object[] { "hello world" })));
            }
            finally
            {
                org.mozilla.javascript.Context.exit();
            }
        }
    }
}
