using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FrontEndMVC.Models;

namespace FrontEndMVC.Controllers
{
    public class ItemTypeController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            ItemType ItemTypeInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/ItemTypes/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    ItemTypeInfo = JsonConvert.DeserializeObject<ItemType>(Response);
                }

            }
            return View(ItemTypeInfo);
        }

        public async Task<ActionResult> Index()
        {
            List<ItemType> ItemTypeInfo = new List<ItemType>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/ItemTypes");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemTypeResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    ItemTypeInfo = JsonConvert.DeserializeObject<List<ItemType>>(ItemTypeResponse);

                }
                //returning the storage list to view  
                return View(ItemTypeInfo);
            }

        }
        // GET: ItemTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

        

        // GET: ItemType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemType/Create
        [HttpPost]
        public ActionResult Create(ItemType itemType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/ItemTypes");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ItemType>("ItemTypes/", itemType);
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

        // GET: ItemType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: ItemType/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, ItemType itemType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ItemType>("api/ItemTypes/" + id, itemType);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(itemType);
        }

        // GET: ItemType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: ItemType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/ItemTypes/" + id.ToString());
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
