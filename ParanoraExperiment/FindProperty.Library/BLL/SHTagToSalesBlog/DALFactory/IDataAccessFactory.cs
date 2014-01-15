using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DALFactory
{
    public interface IDataAccessFactory
    {
        ITagMain TagMain();


        IDAL.ITagGscp TagGscp();

        IDAL.ITagMkt TagMkt();

        IDAL.ITagArea TagArea();

        IDAL.ITagAgent TagAgent();
    }
}
