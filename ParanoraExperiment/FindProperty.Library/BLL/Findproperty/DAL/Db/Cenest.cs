using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Cenest:IDAL.ICenest
    {
        public List<ViewModel.Cenest> SelectCenest(string cestCode)
        {
            throw new Exception();
        }


        public ViewModel.Cenest GetCenest(string cestCode)
        {
            ViewModel.Cenest model = null;
            string sql1 = "select top 1 c_estate,pc_dev,tot_unit,mgt_com,mgt_price from dbo.cenest";
            if (!string.IsNullOrEmpty(cestCode))
            {
                sql1 += " where cestcode='" + cestCode + "' ";
            }
            string sql2 = "select top 1 floorratio,greenratio,RTRIM(bus) bus,RTRIM(railway) railway,RTRIM(schoolkid) schoolkid,RTRIM(schoolpri) schoolpri,RTRIM(schoolsec) schoolsec,RTRIM(schooloth) schooloth,RTRIM(shop) shop,RTRIM(food) food,RTRIM(bank) bank,RTRIM(hospital) hospital from dbo.Estinfo";
            //RTRIM(bus),RTRIM(railway),RTRIM(schoolkid),RTRIM(schoolpri),RTRIM(schoolsec),RTRIM(schooloth),RTRIM(shop),RTRIM(food),RTRIM(bank),RTRIM(hospital)
            //bus,railway,schoolkid,schoolpri,schoolsec,schooloth,shop,food,bank,hospital
            if (!string.IsNullOrEmpty(cestCode))
            {
                sql2 += " where cestcode='" + cestCode + "' ";
            }

            List<ViewModel.Post> result = new List<ViewModel.Post>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql1+sql2);
            if (ds != null && ds.Tables.Count > 0)
            {
                var cenests=new DataTableToModeList<ViewModel.Cenest>().Convert(ds.Tables[0]);
                if(cenests!=null&&cenests.Count()>0)
                {
                    model = cenests[0];
                }
                var estInfos = new DataTableToModeList<ViewModel.EstInfo>().Convert(ds.Tables[1]);
                if (estInfos != null && estInfos.Count() > 0)
                {
                    model.EstInfo = estInfos[0];
                }                
            }
            return model;
        }
    }
}
