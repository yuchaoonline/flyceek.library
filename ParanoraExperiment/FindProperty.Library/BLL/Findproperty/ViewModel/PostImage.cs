
using System;
namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
	/// <summary>
	/// PostImage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PostImage
	{
		public PostImage()
		{}
		#region Model
		private Guid _id;
		private int _seq;
		private string _path;
		private string _description;
		private string _cdescription;
		private string _reftype;
		/// <summary>
		/// 
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
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
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CDescription
		{
			set{ _cdescription=value;}
			get{return _cdescription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RefType
		{
			set{ _reftype=value;}
			get{return _reftype;}
		}
		#endregion Model

	}
}

