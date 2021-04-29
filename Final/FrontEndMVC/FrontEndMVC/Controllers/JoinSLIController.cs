using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;
namespace FrontEndMVC.Controllers
{
    public class JoinSLIController : Controller
    {
        // GET: JoinSLI
        public ActionResult Index()
        {
            IEnumerable<JoinSLI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinSLITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinSLI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
        }
        public ActionResult FilterByShoppingList(int? id)
        {
            IEnumerable<JoinSLI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinSLITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinSLI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc.Where(Item => Item.GetShoppingListItem.SLID == id));
        }
    }
}