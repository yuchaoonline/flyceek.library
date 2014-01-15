using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class PostHitCount:IDAL.IPostHitCount
    {
        public void Add(Model.PostHitCount model)
        {
            string history = model.ToInsertString();

            if (string.IsNullOrEmpty(history)) return;
            string queryStringName = ConfigInfo.HitCountMSMQPath;
            using (MessageQueue queue = new MessageQueue(queryStringName))
            {
                queue.DefaultPropertiesToSend.Recoverable = true;
                Message ms = new Message(history, new BinaryMessageFormatter());
                queue.Send(ms);
            }
            Model.PostHitCountW modelW = new Model.PostHitCountW();
            modelW.ID = model.ID;
            modelW.AgentNo = model.AgentNo;
            modelW.cestcode = model.cestcode;
            modelW.cuntcode = model.cuntcode;
            modelW.CostCent = model.CostCent;
            modelW.ClientIP = model.IP;
            modelW.HitTime = model.HitTime;
            modelW.Session = model.Session;
            modelW.Price = model.Price;
            modelW.Rental = model.Rental;
            modelW.WebSite = model.WebSite;
            Add(modelW);
        }

        public void Add(Model.PostHitCountW model)
        {
            string history = model.ToInsertString();

            if (string.IsNullOrEmpty(history)) return;
            string queryStringName = ConfigInfo.HitCountWMSMQPath;
            using (MessageQueue queue = new MessageQueue(queryStringName))
            {
                queue.DefaultPropertiesToSend.Recoverable = true;
                Message ms = new Message(history, new BinaryMessageFormatter());
                queue.Send(ms);
            }
        }
    }
}
