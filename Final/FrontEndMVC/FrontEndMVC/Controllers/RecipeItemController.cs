using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace FrontEndMVC.Controllers
{


    public class RecipeItemController : Controller
    {

        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            RecipeItem RecipeItemInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/RecipeItems/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    RecipeItemInfo = JsonConvert.DeserializeObject<RecipeItem>(Response);
                }

            }
            return View(RecipeItemInfo);
        }
        // GET: RecipeItem
        public async Task<ActionResult> Index()
        {
            List<RecipeItem> RecipeItemInfo = new List<RecipeItem>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/RecipeItems");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    RecipeItemInfo = JsonConvert.DeserializeObject<List<RecipeItem>>(ItemResponse);

                }
                //returning the storage list to view  
                return View(RecipeItemInfo);
            }

        }


        public async Task<ActionResult> FilterByRecipe(int? id)
        {
            List<RecipeItem> RecipeItemInfo = new List<RecipeItem>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/RecipeItems");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    RecipeItemInfo = JsonConvert.DeserializeObject<List<RecipeItem>>(ItemResponse);

                }
                //returning the storage list to view  
                return View(RecipeItemInfo.Where(Item => Item.RecipeID == id));
            }

        }


        // GET: RecipeItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

        // GET: RecipeItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeItem/Create
        [HttpPost]
        public ActionResult Create(RecipeItem recipeItem)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/RecipeItems");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<RecipeItem>("RecipeItems/", recipeItem);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Index");
        }

        // GET: RecipeItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: RecipeItem/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, RecipeItem recipeItem)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                recipeItem.RecipeIID = (int)id;
                //HTTP POST
                var putTask = client.PutAsJsonAsync<RecipeItem>("api/RecipeItems/" + id, recipeItem);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(recipeItem);
        }

        // GET: RecipeItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: RecipeItem/Delete/5
        // POST: Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/RecipeItems/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
