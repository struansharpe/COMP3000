using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBCRUD;
using DBCRUD.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DBCRUD.Controllers.PageControllers
{
    public class HouseHoldItemsController : Controller
    {
        private COMP3000_SSharpeEntities4 db = new COMP3000_SSharpeEntities4();

        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";


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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            if (houseHoldItem == null)
            {
                return HttpNotFound();
            }
            return View(houseHoldItem);
        }

        // GET: HouseHoldItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseHoldItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HHIID,QTY,SSID,IID,HHID")] HouseHoldItem houseHoldItem)
        {
            if (ModelState.IsValid)
            {
                db.HouseHoldItems.Add(houseHoldItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(houseHoldItem);
        }

        // GET: HouseHoldItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            if (houseHoldItem == null)
            {
                return HttpNotFound();
            }
            return View(houseHoldItem);
        }

        // POST: HouseHoldItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HHIID,QTY,SSID,IID,HHID")] HouseHoldItem houseHoldItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseHoldItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(houseHoldItem);
        }

        // GET: HouseHoldItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            if (houseHoldItem == null)
            {
                return HttpNotFound();
            }
            return View(houseHoldItem);
        }

        // POST: HouseHoldItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            db.HouseHoldItems.Remove(houseHoldItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
