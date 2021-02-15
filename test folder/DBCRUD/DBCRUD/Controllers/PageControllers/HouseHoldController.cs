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
    public class HouseHoldController : Controller
    {
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        private COMP3000_SSharpeEntities db = new COMP3000_SSharpeEntities();
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

        public async Task<ActionResult> Details(int? id)
        {

            HouseHold HHInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/HouseHolds/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    HHInfo = JsonConvert.DeserializeObject<HouseHold>(Response);
                }

            }
            return View(HHInfo);
        }
        // GET: HouseHoldsPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseHoldsPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HHID,HHName,AdminUser,RestrictedUsers")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                db.HouseHolds.Add(houseHold);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(houseHold);
        }

        // GET: HouseHoldsPage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = await db.HouseHolds.FindAsync(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHoldsPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HHID,HHName,AdminUser,RestrictedUsers")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseHold).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(houseHold);
        }

        // GET: HouseHoldsPage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = await db.HouseHolds.FindAsync(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHoldsPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HouseHold houseHold = await db.HouseHolds.FindAsync(id);
            db.HouseHolds.Remove(houseHold);
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
