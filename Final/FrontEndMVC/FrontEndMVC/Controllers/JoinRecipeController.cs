using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;

namespace FrontEndMVC.Controllers
{
    public class JoinRecipeController : Controller
    {
        // GET: JoinRecipe
        public ActionResult Index()
        {
            IEnumerable<JoinRI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinRITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinRI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
            
        }
       
        public ActionResult FilterByRecipe(int? id)
        {
            IEnumerable<JoinRI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinRITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinRI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc.Where(Item => Item.GetRecipeItem.RecipeID == id));

        }

    }
}