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
using Final.Models;

namespace Final.Controllers.APIControllers
{
    public class ItemTypesController : ApiController
    {
        private COMP3000_SSharpeEntities6 db = new COMP3000_SSharpeEntities6();

        // GET: api/ItemTypes
        public IQueryable<ItemType> GetItemTypes()
        {
            return db.ItemTypes;
        }

        // GET: api/ItemTypes/5
        [ResponseType(typeof(ItemType))]
        public async Task<IHttpActionResult> GetItemType(int id)
        {
            ItemType itemType = await db.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            return Ok(itemType);
        }

        // PUT: api/ItemTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemType(int id, ItemType itemType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemType.ITID)
            {
                return BadRequest();
            }

            db.Entry(itemType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemTypeExists(id))
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

        // POST: api/ItemTypes
        [ResponseType(typeof(ItemType))]
        public async Task<IHttpActionResult> PostItemType(ItemType itemType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemTypes.Add(itemType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemTypeExists(itemType.ITID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = itemType.ITID }, itemType);
        }

        // DELETE: api/ItemTypes/5
        [ResponseType(typeof(ItemType))]
        public async Task<IHttpActionResult> DeleteItemType(int id)
        {
            ItemType itemType = await db.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            db.ItemTypes.Remove(itemType);
            await db.SaveChangesAsync();

            return Ok(itemType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemTypeExists(int id)
        {
            return db.ItemTypes.Count(e => e.ITID == id) > 0;
        }
    }
}