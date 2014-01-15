using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class SearchRecommendTag
    {
        public string Tag { get; set; }

        public string TagCategory { get; set; }

        public int Num { get; set; }

        public int Type { get; set; }

        public SearchRecommendTag()
        {
            Type = -1;
        }
    }
}
