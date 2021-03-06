﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndMVC.Models;
using System.Net.Http;

namespace FrontEndMVC.Controllers
{
    public class JoinHHIController : Controller
    {
        // GET: JoinHHI
        public ActionResult Index()
        {
            IEnumerable<JoinHHI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinHHITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinHHI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc);
        }

        public ActionResult FilterByHouseHold(int? id)
        {
            IEnumerable<JoinHHI> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://web.socem.plymouth.ac.uk/FYP/SSharpe/api/");

            var consumeapi = hc.GetAsync("JoinHHITables");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<JoinHHI>>();
                displaydata.Wait();
                jc = displaydata.Result;
            }
            return View(jc.Where(Item => Item.GetHouseHoldItem.HHID == id));
        }
    }
}