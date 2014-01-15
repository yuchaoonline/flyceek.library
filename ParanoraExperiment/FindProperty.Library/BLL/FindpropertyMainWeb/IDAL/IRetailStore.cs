using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.IDAL
{
    public interface IRetailStore
    {
        List<ViewModel.RetailStore> GetTopRetailStoreByArea(int top,string scpMkt,string gscp);
    }
}
