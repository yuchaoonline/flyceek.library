
using System;
namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
	/// <summary>
	/// enquiry:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class Enquiry
	{
		public Enquiry()
		{}
		#region Model
		private int _enquiryid;
		private string _name;
		private string _email;
		private string _contact;
		private string _propertyid;
		private string _detail;
		private string _clientip;
		private string _agentno;
		private DateTime? _enquirydate;
		private string _refno;
		private string _managerno;
		private string _scpid;
		private string _scpmkt;
		private string _source;
		private string _cestcode;
		private string _codetype;
		/// <summary>
		/// 
		/// </summary>
		public int enquiryId
		{
			set{ _enquiryid=value;}
			get{return _enquiryid;}
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
		public string email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string propertyId
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detail
		{
			set{ _detail=value;}
			get{return _detail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string clientIp
		{
			set{ _clientip=value;}
			get{return _clientip;}
		}
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
		public DateTime? enquirydate
		{
			set{ _enquirydate=value;}
			get{return _enquirydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RefNo
		{
			set{ _refno=value;}
			get{return _refno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerNo
		{
			set{ _managerno=value;}
			get{return _managerno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpID
		{
			set{ _scpid=value;}
			get{return _scpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpMkt
		{
			set{ _scpmkt=value;}
			get{return _scpmkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cestcode
		{
			set{ _cestcode=value;}
			get{return _cestcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string codeType
		{
			set{ _codetype=value;}
			get{return _codetype;}
		}
		#endregion Model

	}
}

