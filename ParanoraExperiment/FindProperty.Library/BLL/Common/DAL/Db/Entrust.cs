using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP = FindProperty.Lib.BLL.Findproperty;

namespace FindProperty.Lib.BLL.Common.DAL.Db
{
    public class Entrust:IDAL.IEntrust
    {
        public int Send(Model.Entrust model)
        {
            FP.Model.Enquiry addModel = new FP.Model.Enquiry();

            addModel.AgentNo = model.AgentNo;
            addModel.RefNo = (string.IsNullOrEmpty(model.RefNo)? "NA" : model.RefNo);
            addModel.name = model.SenderName;
            addModel.email = model.SenderEMail;
            addModel.contact = model.SenderMobile;
            addModel.enquirydate = DateTime.Now;
            addModel.detail = model.Content;
            addModel.source = model.Source;
            addModel.clientIp = model.ClientIP;
            addModel.scpID = (string.IsNullOrEmpty(model.ScpID) ? "NA" :model.ScpID);
            addModel.scpMkt = (string.IsNullOrEmpty(model.ScpMkt) ? "NA" :model.ScpMkt);
            addModel.propertyId = (string.IsNullOrEmpty(model.PropertyId )? "NA" : model.PropertyId);
            addModel.cestcode = (string.IsNullOrEmpty(model.Cestcode )? "NA" : model.Cestcode);
            addModel.codeType = (string.IsNullOrEmpty(model.CodeType) ? "NA" : model.CodeType);
            addModel.ManagerNo = (string.IsNullOrEmpty(model.ManagerNo) ? "NA" : model.ManagerNo);

            return new FP.Facade.Enquiry().Add(addModel);
        }
    }
}
