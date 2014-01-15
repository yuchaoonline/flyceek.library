
using System;
namespace FindProperty.Lib.BLL.Cms.ViewModel
{
	/// <summary>
	/// icmArticleProp:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class IcmArticleProp
	{
		public IcmArticleProp()
		{}
		#region Model
		private long _articleid;
		private string _name;
		private string _propvalue;
		/// <summary>
		/// 
		/// </summary>
		public long articleID
		{
			set{ _articleid=value;}
			get{return _articleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string propValue
		{
			set{ _propvalue=value;}
			get{return _propvalue;}
		}
		#endregion Model

	}
}

