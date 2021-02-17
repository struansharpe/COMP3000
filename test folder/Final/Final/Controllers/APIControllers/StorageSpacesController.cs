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
using Final;
using Final.Models;

namespace Final.Controllers.APIControllers
{
    public class StorageSpacesController : ApiController
    {
        private COMP3000_SSharpeEntities3 db = new COMP3000_SSharpeEntities3();

        // GET: api/StorageSpaces
        public IQueryable<StorageSpace> GetStorageSpaces()
        {
            return db.StorageSpaces;
        }

        // GET: api/StorageSpaces/5
        [ResponseType(typeof(StorageSpace))]
        public async Task<IHttpActionResult> GetStorageSpace(int id)
        {
            StorageSpace storageSpace = await db.StorageSpaces.FindAsync(id);
            if (storageSpace == null)
            {
                return NotFound();
            }

            return Ok(storageSpace);
        }

        // PUT: api/StorageSpaces/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorageSpace(int id, StorageSpace storageSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storageSpace.SSID)
            {
                return BadRequest();
            }

            db.Entry(storageSpace).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageSpaceExists(id))
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

        // POST: api/StorageSpaces
        [ResponseType(typeof(StorageSpace))]
        public async Task<IHttpActionResult> PostStorageSpace(StorageSpace storageSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StorageSpaces.Add(storageSpace);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StorageSpaceExists(storageSpace.SSID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storageSpace.SSID }, storageSpace);
        }

        // DELETE: api/StorageSpaces/5
        [ResponseType(typeof(StorageSpace))]
        public async Task<IHttpActionResult> DeleteStorageSpace(int id)
        {
            StorageSpace storageSpace = await db.StorageSpaces.FindAsync(id);
            if (storageSpace == null)
            {
                return NotFound();
            }

            db.StorageSpaces.Remove(storageSpace);
            await db.SaveChangesAsync();

            return Ok(storageSpace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageSpaceExists(int id)
        {
            return db.StorageSpaces.Count(e => e.SSID == id) > 0;
        }
    }
}