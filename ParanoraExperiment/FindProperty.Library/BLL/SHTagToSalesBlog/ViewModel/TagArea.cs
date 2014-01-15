using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.ViewModel
{
    [Serializable]
    public class TagArea
    {
        public TagArea()
		{}
		#region Model
		private string _c_distname;
		private string _scp_mkt;
		private string _regionpy;
		private string _regionpysx;
		private string _cntradi;
		/// <summary>
		/// 
		/// </summary>
		public string c_distname
		{
			set{ _c_distname=value;}
			get{return _c_distname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SCP_MKT
		{
			set{ _scp_mkt=value;}
			get{return _scp_mkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionPY
		{
			set{ _regionpy=value;}
			get{return _regionpy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionPYSX
		{
			set{ _regionpysx=value;}
			get{return _regionpysx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CNTradi
		{
			set{ _cntradi=value;}
			get{return _cntradi;}
		}
		#endregion Model

        public int Num{get;set;}
        public int SNum{get;set;}
        public int RNum{get;set;}

        public MongoDB.Bson.ObjectId _id { get; set; }
    }
}
