﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DBCRUD;
using DBCRUD.Models;

namespace DBCRUD.Controllers
{
    public class HouseHoldItemsController : ApiController
    {
        private COMP3000_SSharpeEntities4 db = new COMP3000_SSharpeEntities4();

        //GET: api/HouseHoldItems
        [HttpGet, ActionName("GetHouseHoldItems")]
        public IQueryable<HouseHoldItem> GetHouseHoldItems()
        {
            return db.HouseHoldItems;
        }
       
        
          //  return db.HouseHoldItems.Where(HouseHoldItem => HouseHoldItem.HHID == 1);
        

        // GET: api/HouseHoldItems/5
        [ResponseType(typeof(HouseHoldItem))]
        public async Task<IHttpActionResult> GetHouseHoldItem(int id)
        {
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            if (houseHoldItem == null)
            {
                return NotFound();
            }

            return Ok(houseHoldItem);
        }

        // PUT: api/HouseHoldItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHouseHoldItem(int id, HouseHoldItem houseHoldItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != houseHoldItem.HHIID)
            {
                return BadRequest();
            }

            db.Entry(houseHoldItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseHoldItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HouseHoldItems
        [ResponseType(typeof(HouseHoldItem))]
        public async Task<IHttpActionResult> PostHouseHoldItem(HouseHoldItem houseHoldItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HouseHoldItems.Add(houseHoldItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HouseHoldItemExists(houseHoldItem.HHIID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = houseHoldItem.HHIID }, houseHoldItem);
        }

        // DELETE: api/HouseHoldItems/5
        [ResponseType(typeof(HouseHoldItem))]
        public async Task<IHttpActionResult> DeleteHouseHoldItem(int id)
        {
            HouseHoldItem houseHoldItem = await db.HouseHoldItems.FindAsync(id);
            if (houseHoldItem == null)
            {
                return NotFound();
            }

            db.HouseHoldItems.Remove(houseHoldItem);
            await db.SaveChangesAsync();

            return Ok(houseHoldItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HouseHoldItemExists(int id)
        {
            return db.HouseHoldItems.Count(e => e.HHIID == id) > 0;
        }
    }
}