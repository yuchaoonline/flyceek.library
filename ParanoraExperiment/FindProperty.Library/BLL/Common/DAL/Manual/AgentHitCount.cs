using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentHitCount:IDAL.IAgentHitCount
    {
        public void Add(Model.AgentHitCount model)
        {
            string history = model.ToInsertString();

            if (string.IsNullOrEmpty(history)) return;
            string queryStringName = ConfigInfo.AgentHitCountMSMQPath;
            using (MessageQueue queue = new MessageQueue(queryStringName))
            {
                queue.DefaultPropertiesToSend.Recoverable = true;
                Message ms = new Message(history, new BinaryMessageFormatter());
                queue.Send(ms);
            }
        }
    }
}
