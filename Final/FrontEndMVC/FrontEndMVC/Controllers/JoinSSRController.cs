using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;

namespace FrontEndMVC.Controllers
{
    public class JoinSSRController : Controller
    {
        // GET: JoinSSR
        public ActionResult Index()
        {
            IEnumerable<JoinSSR> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinSSRTables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinSSR>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
        }

        //public ActionResult FilterByRoom()
        //{
        //    IEnumerable<JoinSSR> jc = null;
        //    HttpClient hc = new HttpClient();
        //    hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

        //    var consumeapi = hc.GetAsync("JoinSSRTables");
        //    consumeapi.Wait();

        //    var readdata = consumeapi.Result;
        //    if (readdata.IsSuccessStatusCode)
        //    {
        //        var displaydata = readdata.Content.ReadAsAsync<IList<JoinSSR>>();
        //        displaydata.Wait();
        //        jc = displaydata.Result;
        //    }
        //    return View(jc);
        //}
    }
}
