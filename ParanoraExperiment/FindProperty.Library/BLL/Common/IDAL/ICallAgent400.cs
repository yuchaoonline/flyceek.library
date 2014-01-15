using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ICallAgent400
    {
        CallAgent400 GetCall400(string post_id,
            string called1,
            string called1_no ,
            string called1_name,
            string called2,
            string called2_no,
            string called2_name,
            string called_message,
            string sericeUrl,
            string auth_consumer_key,
            string consumerSecret);
    }
}
