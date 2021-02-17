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
    public class ItemController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            Item ItemInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Items/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    ItemInfo = JsonConvert.DeserializeObject<Item>(Response);
                }

            }
            return View(ItemInfo);
        }

        public async Task<ActionResult> Index()
        {
            List<Item> ItemInfo = new List<Item>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/Items");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    ItemInfo = JsonConvert.DeserializeObject<List<Item>>(ItemResponse);

                }
                //returning the storage list to view  
                return View(ItemInfo);
            }

        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

       

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/Items");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Item>("Items/", item);
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

        // GET: Item/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: Item/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Item>("api/Items/" + id, item);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Items/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        // filter items by Item Type
        public async Task<ActionResult> FilterByIT(int? id)
        {
            List<Item> ItemInfo = new List<Item>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/Items");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    ItemInfo = JsonConvert.DeserializeObject<List<Item>>(ItemResponse);

                }
                //returning the storage list to view  
                return View(ItemInfo.Where(Item => Item.ITID == id));
            }

        }
    }
}
