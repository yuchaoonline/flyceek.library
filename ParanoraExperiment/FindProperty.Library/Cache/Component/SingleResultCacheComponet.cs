using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Cache.Provider;
using System.Collections;
using FindProperty.Lib.Common;
using FindProperty.Lib.Cache.Component.CacheKeyCreator;

namespace FindProperty.Lib.Cache.Component
{
    public class SingleResultCacheComponet : CacheComponetFundation
    {
        public SingleResultCacheComponet(ICache cacheProvider)
            : base(cacheProvider)
        {
        }

        public SingleResultCacheComponet()
            : base()
        {
        }
    }
}
