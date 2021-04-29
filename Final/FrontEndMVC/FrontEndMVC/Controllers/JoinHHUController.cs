using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;

namespace FrontEndMVC.Controllers
{
    public class JoinHHUController : Controller
    {
        // GET: JoinHHU
        public ActionResult Index()
        {
            IEnumerable<JoinHHU> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinHHUTables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinHHU>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
        }
    }
}