
using System;
namespace FindProperty.Lib.BLL.Cms.Model
{
	/// <summary>
	/// icmArticle:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class IcmArticle
	{
		public IcmArticle()
		{}
		#region Model
		private long _articleid;
		private long _seriesid;
		private long? _userid;
		private string _subject;
		private string _title;
		private string _body;
		private long _creationdate;
		private long _modifieddate;
		private long _publishdate;
		private long _expiredate;
		private int _modvalue;
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
		public long seriesID
		{
			set{ _seriesid=value;}
			get{return _seriesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string subject
		{
			set{ _subject=value;}
			get{return _subject;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string body
		{
			set{ _body=value;}
			get{return _body;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long creationDate
		{
			set{ _creationdate=value;}
			get{return _creationdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long modifiedDate
		{
			set{ _modifieddate=value;}
			get{return _modifieddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long publishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long expireDate
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int modValue
		{
			set{ _modvalue=value;}
			get{return _modvalue;}
		}
		#endregion Model

	}
}

