﻿using System;
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
    public class ShoppingListController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";
        // GET: ShoppingList
        public async Task<ActionResult> Index()
        {
            List<ShoppingList> SLInfo = new List<ShoppingList>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/ShoppingLists");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StorageSpaceResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    SLInfo = JsonConvert.DeserializeObject<List<ShoppingList>>(StorageSpaceResponse);

                }
                //returning the storage list to view  
                return View(SLInfo);
            }

        }

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            ShoppingList SLInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/ShoppingLists/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    SLInfo = JsonConvert.DeserializeObject<ShoppingList>(Response);
                }

            }
            return View(SLInfo);
        }

       

        // GET: ShoppingList/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetDeserialize(id);
        }

        // GET: ShoppingList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingList/Create
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

        // GET: ShoppingList/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetDeserialize(id);
        }

        // POST: ShoppingList/Edit/5
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

        // GET: ShoppingList/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return await GetDeserialize(id);
        }

        // POST: ShoppingList/Delete/5
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
