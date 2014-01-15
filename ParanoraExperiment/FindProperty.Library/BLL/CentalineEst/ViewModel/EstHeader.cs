
using System;
namespace FindProperty.Lib.BLL.CentalineEst.ViewModel
{
	/// <summary>
	/// EstHeader:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EstHeader
	{
		public EstHeader()
		{}
		#region Model
		private string _estcode;
		private string _estname="";
		private string _nickestname="";
		private string _spell;
		private string _title;
		private DateTime _createdate= DateTime.Now;
		private DateTime _updatedate= DateTime.Now;
		private decimal? _price=0M;
		private string _distname;
		private string _thumbpath;
		private string _scpmkt;
		private decimal? _unitprice=0M;
		/// <summary>
		/// 
		/// </summary>
		public string EstCode
		{
			set{ _estcode=value;}
			get{return _estcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EstName
		{
			set{ _estname=value;}
			get{return _estname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NickEstName
		{
			set{ _nickestname=value;}
			get{return _nickestname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Spell
		{
			set{ _spell=value;}
			get{return _spell;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Distname
		{
			set{ _distname=value;}
			get{return _distname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ThumbPath
		{
			set{ _thumbpath=value;}
			get{return _thumbpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScpMkt
		{
			set{ _scpmkt=value;}
			get{return _scpmkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UnitPrice
		{
			set{ _unitprice=value;}
			get{return _unitprice;}
		}
		#endregion Model

	}
}

