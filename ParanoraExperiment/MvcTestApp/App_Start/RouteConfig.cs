using MvcTestApp.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcTestApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.RouteExistingFiles = true;

            //routes.Add(new UrlProvider());

            routes.MapRoute(
                name: "houstList",
                url: "house/list/{no}",
                defaults: new { controller = "house", action = "list", no = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller="Home", action="Index", id= UrlParameter.Optional }// 参数默认值
            );

            routes.MapRoute(
                "DefaultHtml3", // id伪静态
                "{controller}/{action}/{id}.html",// 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }// 参数默认值
            );
        }
    }
}