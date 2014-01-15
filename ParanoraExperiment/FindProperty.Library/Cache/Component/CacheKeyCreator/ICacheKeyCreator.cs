using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.CacheKeyCreator
{
    public interface ICacheKeyCreator
    {
        string CreateKey(params object[] appendArgs);
    }
}
