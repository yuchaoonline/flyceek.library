using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.Model
{
    public class Entrust
    {
        public string  AgentNo{get;set;} 
        public string  RefNo{get;set;}         
        public string  SenderName{get;set;}  
        public string  SenderMobile{get;set;}  
        public string  SenderEMail{get;set;}  
        public string  Content{get;set;}  
        public string  Source{get;set;}
        public string  ClientIP { get; set; }

        public string AgentEmail { get; set; }
        public string AgentCName { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerCName { get; set; }
        public string ManagerNo { get; set; }
        public string ScpID { get; set; }

        public string ScpMkt { get; set; }

        public string PropertyId { get; set; }

        public string Cestcode { get; set; }
        public string CodeType { get; set; }
    }
}
