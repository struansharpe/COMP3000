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
    public class StorageSpacesController : Controller
    {
        private COMP3000_SSharpeEntities3 db = new COMP3000_SSharpeEntities3();

        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

       
        public async Task<ActionResult> Index()
        {
            List<StorageSpace> StorageSpaceInfo = new List<StorageSpace>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/StorageSpaces");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StorageSpaceResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the storage list  
                    StorageSpaceInfo = JsonConvert.DeserializeObject<List<StorageSpace>>(StorageSpaceResponse);

                }
                //returning the storage list to view  
                return View(StorageSpaceInfo);
            }

        }

        // GET: StorageSpaces/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            StorageSpace SSInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/StorageSpaces/" + id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                   SSInfo = JsonConvert.DeserializeObject<StorageSpace>(Response);
                }

            }
            return View(SSInfo);
        }

        // GET: StorageSpaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StorageSpaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SSID,StorageName,RID")] StorageSpace storageSpace)
        {
            if (ModelState.IsValid)
            {
                db.StorageSpaces.Add(storageSpace);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(storageSpace);
        }

        // GET: StorageSpaces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageSpace storageSpace = await db.StorageSpaces.FindAsync(id);
            if (storageSpace == null)
            {
                return HttpNotFound();
            }
            return View(storageSpace);
        }

        // POST: StorageSpaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SSID,StorageName,RID")] StorageSpace storageSpace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storageSpace).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(storageSpace);
        }

        // GET: StorageSpaces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageSpace storageSpace = await db.StorageSpaces.FindAsync(id);
            if (storageSpace == null)
            {
                return HttpNotFound();
            }
            return View(storageSpace);
        }

        // POST: StorageSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StorageSpace storageSpace = await db.StorageSpaces.FindAsync(id);
            db.StorageSpaces.Remove(storageSpace);
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
