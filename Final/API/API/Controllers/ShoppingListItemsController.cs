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
    public class ShoppingListItemsController : ApiController
    {
        private COMP3000_SSharpeEntities14 db = new COMP3000_SSharpeEntities14();

        // GET: api/ShoppingListItems1
        public IQueryable<ShoppingListItem> GetShoppingListItems()
        {
            return db.ShoppingListItems;
        }

        // GET: api/ShoppingListItems1/5
        [ResponseType(typeof(ShoppingListItem))]
        public async Task<IHttpActionResult> GetShoppingListItem(int id)
        {
            ShoppingListItem shoppingListItem = await db.ShoppingListItems.FindAsync(id);
            if (shoppingListItem == null)
            {
                return NotFound();
            }

            return Ok(shoppingListItem);
        }

        // PUT: api/ShoppingListItems1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShoppingListItem(int id, ShoppingListItem shoppingListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingListItem.SLIID)
            {
                return BadRequest();
            }

            db.Entry(shoppingListItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListItemExists(id))
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

        // POST: api/ShoppingListItems1
        [ResponseType(typeof(ShoppingListItem))]
        public async Task<IHttpActionResult> PostShoppingListItem(ShoppingListItem shoppingListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingListItems.Add(shoppingListItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = shoppingListItem.SLIID }, shoppingListItem);
        }

        // DELETE: api/ShoppingListItems1/5
        [ResponseType(typeof(ShoppingListItem))]
        public async Task<IHttpActionResult> DeleteShoppingListItem(int id)
        {
            ShoppingListItem shoppingListItem = await db.ShoppingListItems.FindAsync(id);
            if (shoppingListItem == null)
            {
                return NotFound();
            }

            db.ShoppingListItems.Remove(shoppingListItem);
            await db.SaveChangesAsync();

            return Ok(shoppingListItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingListItemExists(int id)
        {
            return db.ShoppingListItems.Count(e => e.SLIID == id) > 0;
        }
    }
}