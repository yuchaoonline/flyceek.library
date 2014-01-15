using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Enquiry
    {
        private IDAL.IEnquiry dal = DALFactory.DataAccessFactoryCreator.Create().Enquiry();

        public int Add(Model.Enquiry model)
        {
            return dal.Add(model);
        }
    }
}
