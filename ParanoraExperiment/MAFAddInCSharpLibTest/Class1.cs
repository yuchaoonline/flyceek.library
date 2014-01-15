using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAFAddInViewsCSharpLibTest;

namespace MAFAddInCSharpLibTest
{
    [AddIn("AddInTest1", Publisher = "Paranora", Version = "0.0.0.0", Description = "AddInTest1")]
    public class AddIn1:View1
    {
        public override string Operate(string param)
        {
            return param;
        }
    }
}
