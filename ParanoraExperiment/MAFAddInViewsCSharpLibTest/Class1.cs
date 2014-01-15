using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAFAddInViewsCSharpLibTest
{
    [AddInBase]
    public abstract class View1
    {
        public abstract string Operate(string param);
    }
}
