using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTfulTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
