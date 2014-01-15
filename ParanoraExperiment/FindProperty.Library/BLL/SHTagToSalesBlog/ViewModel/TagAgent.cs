
using System;
namespace FindProperty.Lib.BLL.SHTagToSalesBlog.ViewModel
{
	/// <summary>
	/// TagAgent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TagAgent
	{
		public TagAgent()
		{}
		#region Model
		private string _agentno;
		private string _agentcname;
		private string _agentcnpysx;
		private string _agentcnpy;
		private string _agentmobile;
		private string _agentcnt;
		private int _aboutnum;
		private int _snum;
		private int _rnum;
		/// <summary>
		/// 
		/// </summary>
		public string AgentNo
		{
			set{ _agentno=value;}
			get{return _agentno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentCName
		{
			set{ _agentcname=value;}
			get{return _agentcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentCNPYSX
		{
			set{ _agentcnpysx=value;}
			get{return _agentcnpysx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentCNPY
		{
			set{ _agentcnpy=value;}
			get{return _agentcnpy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentMobile
		{
			set{ _agentmobile=value;}
			get{return _agentmobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentCNT
		{
			set{ _agentcnt=value;}
			get{return _agentcnt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AboutNum
		{
			set{ _aboutnum=value;}
			get{return _aboutnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SNum
		{
			set{ _snum=value;}
			get{return _snum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RNum
		{
			set{ _rnum=value;}
			get{return _rnum;}
		}
		#endregion Model

        public MongoDB.Bson.ObjectId _id { get; set; }
	}
}

