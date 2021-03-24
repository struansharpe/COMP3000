using MVCDataPractice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCDataPractice.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Hosted web API REST Service base url  
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/api";
        public async Task<ActionResult> Index()
        {
            List<Storage> StoreInfo = new List<Storage>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/Storages/GetStorages");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StoreResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    StoreInfo = JsonConvert.DeserializeObject<List<Storage>>(StoreResponse);

                }
                //returning the employee list to view  
                return View(StoreInfo);
            }
        }
    }
}