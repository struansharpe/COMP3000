using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEndMVC.Controllers
{
    public class ShoppingListPagesController : Controller
    {
        // GET: ShoppingListPages
        public ActionResult Index()
        {
            return View();
        }
    }
}