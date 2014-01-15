using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db
{
    public class TagArea:IDAL.ITagArea
    {
        public List<ViewModel.TagArea> SelectTagArea(string distname)
        {
            string sql = "SELECT b.*,a.Num,a.SNum,a.RNum FROM tbl_AreaAbuotNum a JOIN  tbl_RegionTemp b ON a.SCP_MKT=b.SCP_MKT";
            string where=" where 1=1 ";
            if(!string.IsNullOrEmpty(distname))
            {
                where+=" and c_distname='"+distname+"'";
                where+=" and RegionPY ='"+distname.ToLower()+"'";
                where+=" and RegionPYSX = '"+distname.ToUpper()+"'";
                where+=" and CNTradi='"+distname+"'";
            }
            List<ViewModel.TagArea> list=new List<ViewModel.TagArea>();
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagArea>().Convert(ds.Tables[0]);
            }
            return list;
        }


        public List<ViewModel.TagArea> SelectTagAreaByDistname(string distname)
        {
            string sql = "SELECT b.*,a.Num,a.SNum,a.RNum FROM tbl_AreaAbuotNum a JOIN  tbl_RegionTemp b ON a.SCP_MKT=b.SCP_MKT";
            string where=" where 1=1";
            if(!string.IsNullOrEmpty(distname))
            {
                where+=" and c_distname='"+distname+"'";
            }
            List<ViewModel.TagArea> list=new List<ViewModel.TagArea>();
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagArea>().Convert(ds.Tables[0]);
            }
            return list;
        }
    }
}
