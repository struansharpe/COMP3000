using System;
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
using API.Models;

namespace API.Controllers
{
    public class DesiredStocksController : ApiController
    {
        private COMP3000_SSharpeEntities13 db = new COMP3000_SSharpeEntities13();

        // GET: api/DesiredStocks
        public IQueryable<DesiredStock> GetDesiredStocks()
        {
            return db.DesiredStocks;
        }

        // GET: api/DesiredStocks/5
        [ResponseType(typeof(DesiredStock))]
        public async Task<IHttpActionResult> GetDesiredStock(int id)
        {
            DesiredStock desiredStock = await db.DesiredStocks.FindAsync(id);
            if (desiredStock == null)
            {
                return NotFound();
            }

            return Ok(desiredStock);
        }

        // PUT: api/DesiredStocks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDesiredStock(int id, DesiredStock desiredStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != desiredStock.IID)
            {
                return BadRequest();
            }

            db.Entry(desiredStock).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesiredStockExists(id))
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

        // POST: api/DesiredStocks
        [ResponseType(typeof(DesiredStock))]
        public async Task<IHttpActionResult> PostDesiredStock(DesiredStock desiredStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DesiredStocks.Add(desiredStock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DesiredStockExists(desiredStock.IID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = desiredStock.IID }, desiredStock);
        }

        // DELETE: api/DesiredStocks/5
        [ResponseType(typeof(DesiredStock))]
        public async Task<IHttpActionResult> DeleteDesiredStock(int id)
        {
            DesiredStock desiredStock = await db.DesiredStocks.FindAsync(id);
            if (desiredStock == null)
            {
                return NotFound();
            }

            db.DesiredStocks.Remove(desiredStock);
            await db.SaveChangesAsync();

            return Ok(desiredStock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesiredStockExists(int id)
        {
            return db.DesiredStocks.Count(e => e.IID == id) > 0;
        }
    }
}