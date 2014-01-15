using FindProperty.Lib.Cache.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component
{
    public class ListResultCacheComponet : CacheComponetFundation
    {
        public ListResultCacheComponet(ICache cacheProvider)
            : base(cacheProvider)
        {
        }

        public ListResultCacheComponet()
            : base()
        {
        }
    }
}
