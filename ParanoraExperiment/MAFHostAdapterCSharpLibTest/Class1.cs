using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAFContractCSharpLibTest;
using MAFHostViewCSharpLibTest;

namespace MAFHostAdapterCSharpLibTest
{
    [HostAdapter]
    public class HostAdapter1:View1
    {
        private IContract1 contract;
        private ContractHandle handle;

        public HostAdapter1(IContract1 contract)
        {
            this.contract = contract;
            handle = new ContractHandle(contract);
        }

        public override string Operate(string param)
        {
            return contract.Operate(param);
        }
    }
}
