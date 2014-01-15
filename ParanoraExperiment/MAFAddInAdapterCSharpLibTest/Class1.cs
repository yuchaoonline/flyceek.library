using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAFAddInViewsCSharpLibTest;
using MAFContractCSharpLibTest;

namespace MAFAddInAdapterCSharpLibTest
{
    [AddInAdapter]
    internal class AddInAdapter1 : ContractBase, IContract1
    {

        private View1 view;

        public AddInAdapter1(View1 view)
        {
            this.view = view;
        }

        public string Operate(string param)
        {
            return view.Operate(param);
        }
    }
}
