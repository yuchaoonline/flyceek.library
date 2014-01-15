using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcTestApp.Lib
{
    public class UrlProvider:RouteBase
    {
        private static PropertyInfo s_isReadOnlyPropertyInfo;

        public UrlProvider()
        {
            Type type = typeof(NameObjectCollectionBase);
            s_isReadOnlyPropertyInfo = type.GetProperty(
                "IsReadOnly",
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static void ProcessCollection(NameValueCollection collection)
        {
            var copy = new NameValueCollection();

            foreach (string key in collection.AllKeys)
            {
                Array.ForEach(
                    collection.GetValues(key),
                    v => copy.Add(key, v));
            }

            // set readonly to false.
            s_isReadOnlyPropertyInfo.SetValue(collection, false, null);

            collection.Clear();
            collection.Add(copy);

            // set readonly to true.
            s_isReadOnlyPropertyInfo.SetValue(collection, true, null);
        } 

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "house");
            data.Values.Add("action", "list");
            data.Values.Add("no", "leng");

            NameValueCollection collection = httpContext.Request.QueryString;
            var copy = new NameValueCollection();

            foreach (string key in collection.AllKeys)
            {
                Array.ForEach(
                    collection.GetValues(key),
                    v => copy.Add(key, v));
            }
            copy.Add("a", "Flyceek");
            // set readonly to false.
            s_isReadOnlyPropertyInfo.SetValue(collection, false, null);

            collection.Clear();
            collection.Add(copy);

            // set readonly to true.
            s_isReadOnlyPropertyInfo.SetValue(collection, true, null);

            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            throw new NotImplementedException();
        }
    }
}