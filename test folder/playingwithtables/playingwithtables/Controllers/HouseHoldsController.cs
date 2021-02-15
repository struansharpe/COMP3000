using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using playingwithtables;
using playingwithtables.Models;

namespace playingwithtables.Controllers
{
    public class HouseHoldsController : ApiController
    {
        private COMP3000_SSharpeEntities2 db = new COMP3000_SSharpeEntities2();

        // GET: api/HouseHolds
        public IQueryable<HouseHold> GetHouseHolds()
        {
            return db.HouseHolds;
        }

        // GET: api/HouseHolds/5
        [ResponseType(typeof(HouseHold))]
        public IHttpActionResult GetHouseHold(int id)
        {
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return NotFound();
            }

            return Ok(houseHold);
        }

        // PUT: api/HouseHolds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHouseHold(int id, HouseHold houseHold)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != houseHold.HHID)
            {
                return BadRequest();
            }

            db.Entry(houseHold).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseHoldExists(id))
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

        // POST: api/HouseHolds
        [ResponseType(typeof(HouseHold))]
        public IHttpActionResult PostHouseHold(HouseHold houseHold)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HouseHolds.Add(houseHold);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HouseHoldExists(houseHold.HHID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = houseHold.HHID }, houseHold);
        }

        // DELETE: api/HouseHolds/5
        [ResponseType(typeof(HouseHold))]
        public IHttpActionResult DeleteHouseHold(int id)
        {
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return NotFound();
            }

            db.HouseHolds.Remove(houseHold);
            db.SaveChanges();

            return Ok(houseHold);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HouseHoldExists(int id)
        {
            return db.HouseHolds.Count(e => e.HHID == id) > 0;
        }
    }
}