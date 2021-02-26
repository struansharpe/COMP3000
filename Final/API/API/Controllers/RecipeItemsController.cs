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
    public class RecipeItemsController : ApiController
    {
        private COMP3000_SSharpeEntities10 db = new COMP3000_SSharpeEntities10();

        // GET: api/RecipeItems
        public IQueryable<RecipeItem> GetRecipeItems()
        {
            return db.RecipeItems;
        }

        // GET: api/RecipeItems/5
        [ResponseType(typeof(RecipeItem))]
        public async Task<IHttpActionResult> GetRecipeItem(int id)
        {
            RecipeItem recipeItem = await db.RecipeItems.FindAsync(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            return Ok(recipeItem);
        }

        // PUT: api/RecipeItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRecipeItem(int id, RecipeItem recipeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipeItem.RecipeIID)
            {
                return BadRequest();
            }

            db.Entry(recipeItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(id))
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

        // POST: api/RecipeItems
        [ResponseType(typeof(RecipeItem))]
        public async Task<IHttpActionResult> PostRecipeItem(RecipeItem recipeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RecipeItems.Add(recipeItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = recipeItem.RecipeIID }, recipeItem);
        }

        // DELETE: api/RecipeItems/5
        [ResponseType(typeof(RecipeItem))]
        public async Task<IHttpActionResult> DeleteRecipeItem(int id)
        {
            RecipeItem recipeItem = await db.RecipeItems.FindAsync(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            db.RecipeItems.Remove(recipeItem);
            await db.SaveChangesAsync();

            return Ok(recipeItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeItemExists(int id)
        {
            return db.RecipeItems.Count(e => e.RecipeIID == id) > 0;
        }
    }
}