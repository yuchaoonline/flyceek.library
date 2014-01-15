using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class SearchAutoComplete
    {
        public string Tag { get; set; }

        public int AboutNum { get; set; }

        public int CategoryRank { get; set; }

        public SearchAutoComplete()
        {
        }
    }
}
