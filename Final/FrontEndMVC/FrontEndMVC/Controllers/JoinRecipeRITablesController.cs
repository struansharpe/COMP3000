using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;

namespace FrontEndMVC.Controllers
{
    public class JoinRecipeRITablesController : Controller
    {
        // GET: JoinRecipeRITables
        public ActionResult Index()
        {
            IEnumerable<JoinRecipeRI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinRecipeRI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinRecipeRI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
        }
        public ActionResult FilterByRecipe(int? id)
        {
            IEnumerable<JoinRecipeRI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinRecipeRI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinRecipeRI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc.Where(Item => Item.GetRecipe.RecipeID == id));

        }
    }
}