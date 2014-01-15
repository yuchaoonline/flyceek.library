using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.Facade
{
    public class TagArea
    {
        private IDAL.ITagArea dal = DALFactory.DataAccessFactoryCreator.Create().TagArea();

        public List<ViewModel.TagArea> SelectTagArea(string distname)
        {
            return dal.SelectTagArea(distname);
        }

        public List<ViewModel.TagArea> SelectTagAreaByDistname(string distname)
        {
            return dal.SelectTagAreaByDistname(distname);
        }
    }
}
