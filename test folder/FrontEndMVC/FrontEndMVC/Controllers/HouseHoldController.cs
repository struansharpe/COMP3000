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
    public class HouseHoldController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            HouseHold HHInfo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Households/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    HHInfo = JsonConvert.DeserializeObject<HouseHold>(UserResponse);
                }
            }
            return View(HHInfo);
        }

        public async Task<ActionResult> Index()
        {
            List<HouseHold> HouseHoldInfo = new List<HouseHold>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/HouseHolds");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var HouseHoldResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    HouseHoldInfo = JsonConvert.DeserializeObject<List<HouseHold>>(HouseHoldResponse);

                }
                //returning the storage list to view  
                return View(HouseHoldInfo);
            }

        }
        // GET: Household/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

        

        // GET: HouseHold/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseHold/Create
        [HttpPost]
        public ActionResult Create(HouseHold houseHold)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/HouseHolds");

                //HTTP POST
                var postTask =  client.PostAsJsonAsync<HouseHold>("HouseHolds/", houseHold);
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

        // GET: HouseHold/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: HouseHold/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, HouseHold houseHold)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<HouseHold>("api/HouseHolds/" + id, houseHold);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(houseHold);
        }

        // GET: HouseHold/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }

        // POST: HouseHold/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/HouseHolds/" + id.ToString());
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
