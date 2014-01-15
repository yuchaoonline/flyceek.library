using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.DALFactory
{
    public interface IDataAccessFactory
    {
        IDAL.IRetailStore RetailStore();
    }
}
