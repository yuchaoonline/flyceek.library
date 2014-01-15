
using System;
namespace FindProperty.Lib.BLL.Findproperty.Model
{
	/// <summary>
	/// Scp_id:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Scp
	{
		public Scp()
		{}
		#region Model
		private string _scp_id;
		private string _scp_c;
		private string _scp_e;
		private string _scp_mkt;
		private string _gscp_id;
		private int _displayorder=0;
		private bool _flagdeleted;
		private DateTime? _lastupdate;
		/// <summary>
		/// 
		/// </summary>
		public string scp_id
		{
			set{ _scp_id=value;}
			get{return _scp_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_c
		{
			set{ _scp_c=value;}
			get{return _scp_c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_e
		{
			set{ _scp_e=value;}
			get{return _scp_e;}
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
		public string gscp_id
		{
			set{ _gscp_id=value;}
			get{return _gscp_id;}
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

