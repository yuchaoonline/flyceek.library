
using System;
namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
	/// <summary>
	/// PostItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PostItem
	{
		public PostItem()
		{}
		#region Model
        private Guid _id;
		private string _itemtype;
		private int _seq;
		private string _display;
		private string _cdisplay;
		private string _value;
		private string _cvalue;
		private string _tag;
		private decimal _searchkey;
		/// <summary>
		/// 
		/// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string ItemType
		{
			set{ _itemtype=value;}
			get{return _itemtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Seq
		{
			set{ _seq=value;}
			get{return _seq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Display
		{
			set{ _display=value;}
			get{return _display;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CDisplay
		{
			set{ _cdisplay=value;}
			get{return _cdisplay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CValue
		{
			set{ _cvalue=value;}
			get{return _cvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tag
		{
			set{ _tag=value;}
			get{return _tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal SearchKey
		{
			set{ _searchkey=value;}
			get{return _searchkey;}
		}
		#endregion Model

	}
}

