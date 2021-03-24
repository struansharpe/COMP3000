using FrontEndMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace FrontEndMVC.Controllers
{
    public class ShoppingListItemController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";
        // GET: ShoppingListItem
        public async Task<ActionResult> Index()
        {
            List<ShoppingListItem> SLIInfo = new List<ShoppingListItem>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/ShoppingListItems");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StorageSpaceResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    SLIInfo = JsonConvert.DeserializeObject<List<ShoppingListItem>>(StorageSpaceResponse);

                }
                //returning the storage list to view  
                return View(SLIInfo);
            }

        }

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            ShoppingListItem SLIInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/ShoppingListItems/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    SLIInfo = JsonConvert.DeserializeObject<ShoppingListItem>(Response);
                }

            }
            return View(SLIInfo);
        }


        public async Task<ActionResult> FilterBySL(int? id)
        {
            List<ShoppingListItem> SLIInfo = new List<ShoppingListItem>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/ShoppingListItems");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StorageSpaceResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    SLIInfo = JsonConvert.DeserializeObject<List<ShoppingListItem>>(StorageSpaceResponse);

                }
                //returning the storage list to view  
                return View(SLIInfo.Where(ShoppingListItem => ShoppingListItem.SLID == id));
            }

        }

        // GET: ShoppingListItem/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetDeserialize(id);
        }

        // GET: ShoppingListItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingListItem/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingListItem/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetDeserialize(id);
        }

        // POST: ShoppingListItem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingListItem/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return await GetDeserialize(id);
        }

        // POST: ShoppingListItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
