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
using System.Diagnostics;


namespace FrontEndMVC.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users URL
        string Baseurl = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/";

        //method to get deserialized Data for individual ID
        private async Task<ActionResult> GetDeserialize(int? id)
        {
            User UserInfo = null;
            //IList<User> UserInfo = new List<User>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.GetAsync("api/Users/" + id);

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



        //private async Task<string> HttpRequest(string endPoint, int? id, string type, User user )
        //{

        //    //initialising client
        //    using (var client = new HttpClient())
        //    {       
        //        //specify base address for client
        //        client.BaseAddress = new Uri(Baseurl);

              

        //        HttpResponseMessage Res = null;

        //        switch (type)
        //        {
        //            case "Get":
        //                client.DefaultRequestHeaders.Clear();
        //                //Define request data format  
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                Res = await client.GetAsync(endPoint+ id);
        //                Debug.WriteLine("This is Get " + endPoint+" " + id);
        //                break;
        //            case "Delete":
        //                Res = await client.DeleteAsync(endPoint + id);
        //                Debug.WriteLine("This is Delete " + endPoint + " " + id);
        //                break;
        //            case "Put":
        //                Res =  await client.PutAsJsonAsync<User>(endPoint + id, user);
        //                Debug.WriteLine("This is Pit " + endPoint + " " + id + " " + user);
        //                break;
        //            case "Post":
        //                Debug.WriteLine("This is Post " + endPoint + " " + user);
        //                var postTask = client.PostAsJsonAsync<User>(endPoint, user);
        //                postTask.Wait();
        //                Res = postTask.Result;
        //                break;
        //        }


        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api                      

        //            return Res.Content.ReadAsStringAsync().Result;
        //        }

        //    }
        //    return null;
        //}


            //method for posting to api endpoint
            private async Task<ActionResult> PostMethod(User user)
        {

            Debug.WriteLine("This is Post " + User);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "Users");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("Users/", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            //  await HttpRequest("/", null, "Post", user);

            return RedirectToAction("Index");
        }


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


                    //string UserResponse = await HttpRequest("", null, "Get",null);


                    //Deserializing the response recieved from web api and storing into the storage list  
                    UsersInfo = JsonConvert.DeserializeObject<List<User>>(UserResponse);


                    //returning the storage list to view  
                    return View(UsersInfo);

                }
                return null;
            }
        }



        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await GetDeserialize(id);
        }

        //GET  Users / edit
        public async Task<ActionResult> Edit(int? id)
        {
            return await GetDeserialize(id);
        }

        //post users / edit
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int? id, User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                user.UID = (int)id;
                //HTTP POST
                var putTask = client.PutAsJsonAsync<User>("api/Users/" + id, user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post : create / user

        [HttpPost]
        public ActionResult Create(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "api/Users");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<User>("Users/", user);
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

        //  var result = await HttpRequest("/", null, "Post", user);

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return await GetDeserialize(id);
        }


        //     POST: Users / Delete / 5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Users/" + id.ToString());
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





//        [HttpPost, ActionName("Delete")]
//        public ActionResult Delete(int? id, FormCollection collection)
//        {
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri(Baseurl);

//                //HTTP DELETE
//                var deleteTask = client.DeleteAsync("api/Users/" + id.ToString());
//                deleteTask.Wait();

//                var result = deleteTask.Result;
//                if (result.IsSuccessStatusCode)
//                {

//                    return RedirectToAction("Index");
//                }
//            }

//            return RedirectToAction("Index");
//        }

//    }
////}

//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public ActionResult DeleteUser(int? id)
//{
//    //User UserInfo = null;
//    ////IList<User> UserInfo = new List<User>();
//    //using (var client = new HttpClient())
//    //{
//    //    //Passing service base url  

//    //    client.BaseAddress = new Uri(Baseurl);

//    //    //Define request data format  

//    //    //Sending request to find web api REST service resource
//    //    var deleteTask = await client.DeleteAsync("api/Users/" + id);
//    //    deleteTask.Wait();

//    //    var result = deleteTask.Result;
//    using (var client = new HttpClient())
//    {
//        client.BaseAddress = new Uri(Baseurl);

//        //HTTP DELETE
//        var deleteTask = client.DeleteAsync("/api/Users/" + id.ToString());
//        deleteTask.Wait();

//        var result = deleteTask.Result;
//        if (result.IsSuccessStatusCode)
//            try
//            {



//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//    }


//}




