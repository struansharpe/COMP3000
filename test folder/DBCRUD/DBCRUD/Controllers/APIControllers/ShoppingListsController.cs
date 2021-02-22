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
using DBCRUD.Models;

namespace DBCRUD.Controllers.APIControllers
{
    public class ShoppingListsController : ApiController
    {
        private COMP3000_SSharpeEntities10 db = new COMP3000_SSharpeEntities10();

        // GET: api/ShoppingLists
        public IQueryable<ShoppingList> GetShoppingLists()
        {
            return db.ShoppingLists;
        }

        // GET: api/ShoppingLists/5
        [ResponseType(typeof(ShoppingList))]
        public async Task<IHttpActionResult> GetShoppingList(int id)
        {
            ShoppingList shoppingList = await db.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return Ok(shoppingList);
        }

        // PUT: api/ShoppingLists/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShoppingList(int id, ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingList.SLID)
            {
                return BadRequest();
            }

            db.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
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

        // POST: api/ShoppingLists
        [ResponseType(typeof(ShoppingList))]
        public async Task<IHttpActionResult> PostShoppingList(ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingLists.Add(shoppingList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingListExists(shoppingList.SLID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shoppingList.SLID }, shoppingList);
        }

        // DELETE: api/ShoppingLists/5
        [ResponseType(typeof(ShoppingList))]
        public async Task<IHttpActionResult> DeleteShoppingList(int id)
        {
            ShoppingList shoppingList = await db.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            db.ShoppingLists.Remove(shoppingList);
            await db.SaveChangesAsync();

            return Ok(shoppingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingListExists(int id)
        {
            return db.ShoppingLists.Count(e => e.SLID == id) > 0;
        }
    }
}