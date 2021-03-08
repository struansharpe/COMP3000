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
    public class DesiredStockController : Controller
    {
        // GET: DesiredStock
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";
        public async Task<ActionResult> Index()
        {
           

            List<DesiredStock> desiredStocks = new List<DesiredStock>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/DesiredStocks");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    desiredStocks = JsonConvert.DeserializeObject<List<DesiredStock>>(UserResponse);

                }
                //returning the storage list to view  
                return View(desiredStocks);
            }

        }

     

        // GET: DesiredStock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DesiredStock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesiredStock/Create
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

        // GET: DesiredStock/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DesiredStock/Edit/5
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

        // GET: DesiredStock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DesiredStock/Delete/5
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
