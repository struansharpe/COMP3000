using FrontEndMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FrontEndMVC.Controllers
{
    public class HouseHoldItemsPagesController : Controller
    {

        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";
        // GET: HouseHoldItemsPages
        public async Task<ActionResult> Index(int? id = 2)
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
                return View(HouseHoldItemInfo.Where(HouseHoldItem => HouseHoldItem.HHID == id));
            }

        }
    }
}