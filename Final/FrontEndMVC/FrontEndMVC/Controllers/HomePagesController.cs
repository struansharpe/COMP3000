using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEndMVC.Controllers
{
    public class HomePagesController : Controller
    {
        // GET: HomePage

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
    }
}