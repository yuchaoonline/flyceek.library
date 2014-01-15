using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class AppUpdate
    {

        private IDAL.IAppUpdate dal = DALFactory.DataAccessFactoryCreator.Create().AppUpdate();

        public String GetApkUpdate(string filepath)
        {

            return dal.GetApkUpdateInfo(filepath);
        }
        public String GetApkNews(string filepath)
        {
            return dal.GetApkNews(filepath);
        }
    }
}
