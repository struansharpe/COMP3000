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
    public class HouseHoldItemController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            HouseHoldItem HHIInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/HouseHoldItems/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    HHIInfo = JsonConvert.DeserializeObject<HouseHoldItem>(Response);
                }

            }
            return View(HHIInfo);
        }

        public async Task<ActionResult> Index()
        {
            List<HouseHoldItem> HouseHoldItemInfo = new List<HouseHoldItem>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/HouseHoldItems");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var HouseHoldItemResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    HouseHoldItemInfo = JsonConvert.DeserializeObject<List<HouseHoldItem>>(HouseHoldItemResponse);

                }
                //returning the storage list to view  
                return View(HouseHoldItemInfo);
            }

        }
        // GET: HouseHoldItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

        

        // GET: HouseHoldItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseHoldItem/Create
        [HttpPost]
        public ActionResult Create(HouseHoldItem houseHoldItem)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/HouseHoldItems");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<HouseHoldItem>("HouseHoldItems/", houseHoldItem);
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

        // GET: HouseHoldItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: HouseHoldItem/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, HouseHoldItem houseHoldItem)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<HouseHoldItem>("api/HouseHoldItems/" + id, houseHoldItem);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(houseHoldItem);
        }

        // GET: HouseHoldItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: HouseHoldItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/HouseHoldItems/" + id.ToString());
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
