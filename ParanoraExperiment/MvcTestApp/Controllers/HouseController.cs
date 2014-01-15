using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTestApp.Controllers
{
    public class HouseController : Controller
    {
        //
        // GET: /House/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string no)
        {
            NameValueCollection qs=HttpContext.Request.QueryString;

            ViewModels.HouseList model = new ViewModels.HouseList();
            model.No = no;

            model.Message = qs["a"];

            return View(model);
        }
    }
}
