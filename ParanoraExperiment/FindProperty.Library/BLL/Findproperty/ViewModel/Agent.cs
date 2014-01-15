using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
    [Serializable]
    public class Agent
    {
        public String AgentCName { get; set; }

        public String AgentName { get; set; }
 
        public String AgentNo { get; set; }

        public String AgentMobile { get; set; }

        public String AgentEmail { get; set; }

        public String BranchCName { get; set; }

        public string ManagerNo { get; set; }

        public int post_counter { get; set; }

        public decimal? agentscore { get; set; }
        public decimal? agentscore1 { get; set; }
        public decimal? agentscore2 { get; set; }
        public decimal? agentscore3 { get; set; }
        public decimal? agentscore4 { get; set; }
        public decimal? agentscore5 { get; set; }

        public List<AgentMainBusinessEstate> AgentEsts { get; set; }
        public List<AgentMainBusinessGscp> Scps { get; set; }
        public List<Awards> Awards{get;set;}

        public string EagleMemberType{get;set;}

        public MongoDB.Bson.ObjectId _id { get; set; }

        public string mainscpid { get; set; }
        public string mainscpmkt { get; set; }
    }

    [Serializable]
	public partial class CentaEagleAwards
	{
		public CentaEagleAwards()
		{}
		#region Model
		private decimal? _autoid;
		private string _competitionyear;
		private string _staffno;
		private string _chinesename;
		private string _englishname;
		private string _eaglemembertype;
		private string _effective;
		private string _salesvolume;
		private string _remarks;
		private string _xxxorginalsequence;
		/// <summary>
		/// 
		/// </summary>
		public decimal? AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompetitionYear
		{
			set{ _competitionyear=value;}
			get{return _competitionyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StaffNo
		{
			set{ _staffno=value;}
			get{return _staffno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChineseName
		{
			set{ _chinesename=value;}
			get{return _chinesename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EnglishName
		{
			set{ _englishname=value;}
			get{return _englishname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EagleMemberType
		{
			set{ _eaglemembertype=value;}
			get{return _eaglemembertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string effective
		{
			set{ _effective=value;}
			get{return _effective;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesVolume
		{
			set{ _salesvolume=value;}
			get{return _salesvolume;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string xxxOrginalSequence
		{
			set{ _xxxorginalsequence=value;}
			get{return _xxxorginalsequence;}
		}
		#endregion Model

	}

    [Serializable]
    public partial class Awards
    {
        public Awards()
        { }
        #region Model
        private string _type;
        private string _narrative;
        private string _desc;
        private string _staff_no;
        private int? _year;
        private int? _month;
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string narrative
        {
            set { _narrative = value; }
            get { return _narrative; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string staff_no
        {
            set { _staff_no = value; }
            get { return _staff_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? month
        {
            set { _month = value; }
            get { return _month; }
        }
        #endregion Model

    }

    [Serializable]
    public class AgentMainBusinessEstate
    {
        public string EstName { get; set; }

        public string Code { get; set; }

        public int SCount { get; set; }

        public int RCount { get; set; }

        public string AgentNo { get; set; }

        public string AgentName { get; set; }
    }

    [Serializable]
    public class AgentMainBusinessGscp
    {
        public string scpid { get; set; }

        public string scp_c { get; set; }

        public string scpMkt { get; set; }

        public string c_distname { get; set; }

        public string AgentNo { get; set; }

        public string AgentName { get; set; }
    }
}
