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
    public class UsersController : Controller
    {
        // GET: Users
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";


        public async Task<ActionResult> Index()
        {
            List<User> UsersInfo = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/Users");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    UsersInfo = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                }
                //returning the storage list to view  
                return View(UsersInfo);
            }

        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            User UserInfo= null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Users/"+ id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    UserInfo = JsonConvert.DeserializeObject<User>(UserResponse);
                }
            
            }
            return View(UserInfo);
        }




        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
