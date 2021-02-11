﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace MVCTest.Controllers
{
    public class StoragesCon : Controller
    {
        private string restURI = "http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/";

        //private string restURI = "https://localhost:44345/api/";

        private string resource = "Storages";

        // GET: StoragesCon
       
        public async Task<ActionResult> Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync(restURI + resource);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    System.Web.UI.WebControls.GridView gView = new System.Web.UI.WebControls.GridView();
                    gView.DataSource = table;
                    gView.DataBind();
                    using (System.IO.StringWriter sw = new System.IO.StringWriter())
                    {
                        using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                        {
                            gView.RenderControl(htw);
                            ViewBag.ReturnedData = sw.ToString();
                        }
                    }
                }
            }
            return View();
        }
    }
}