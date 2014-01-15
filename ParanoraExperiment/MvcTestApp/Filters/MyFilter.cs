using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace MvcTestApp.Filters
{
    public class MyFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //ViewDataDictionary viewData = new ViewDataDictionary();

            //ViewResult result = new ViewResult
            //{
            //    ViewName = "error",
            //    ViewData = viewData,
            //    TempData = actionContext.ControllerContext.RouteData;
            //};

            //actionContext.RouteData.Values["controller"] = "error";
            //actionContext.Result = result;
            //actionContext.ExceptionHandled = true;
            //actionContext.HttpContext.Response.Clear();

            
        }
    }
}