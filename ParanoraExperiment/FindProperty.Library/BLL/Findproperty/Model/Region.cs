
using System;
namespace FindProperty.Lib.BLL.Findproperty.Model
{
	/// <summary>
	/// Region:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Region
	{
		public Region()
		{}
		#region Model
		private string _cityname;
		private string _districtno;
		private string _c_distname;
		private string _e_distname;
		private string _scp_mkt;
		private int _displayorder=0;
		private bool _flagdeleted;
		private DateTime? _lastupdate;
		/// <summary>
		/// 
		/// </summary>
		public string cityname
		{
			set{ _cityname=value;}
			get{return _cityname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string districtno
		{
			set{ _districtno=value;}
			get{return _districtno;}
		}
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
		public string e_distname
		{
			set{ _e_distname=value;}
			get{return _e_distname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_mkt
		{
			set{ _scp_mkt=value;}
			get{return _scp_mkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int displayOrder
		{
			set{ _displayorder=value;}
			get{return _displayorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Flagdeleted
		{
			set{ _flagdeleted=value;}
			get{return _flagdeleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Lastupdate
		{
			set{ _lastupdate=value;}
			get{return _lastupdate;}
		}
		#endregion Model

	}
}

