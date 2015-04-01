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
using TrelloModel;

namespace TrelloWebAPI.Controllers
{
    public class ListController : ApiController
    {
        private TrelloModelDBContainer db = new TrelloModelDBContainer();

        // GET: api/List
        public IQueryable<List> GetList()
        {
            return db.List;
        }

        // GET: api/List/5
        [ResponseType(typeof(List))]
        public async Task<IHttpActionResult> GetList(int id)
        {
            List list = await db.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // PUT: api/List/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutList(int id, List list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != list.ListId)
            {
                return BadRequest();
            }

            db.Entry(list).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
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

        // POST: api/List
        [ResponseType(typeof(List))]
        public async Task<IHttpActionResult> PostList(List list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.List.Add(list);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = list.ListId }, list);
        }

        // DELETE: api/List/5
        [ResponseType(typeof(List))]
        public async Task<IHttpActionResult> DeleteList(int id)
        {
            List list = await db.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            db.List.Remove(list);
            await db.SaveChangesAsync();

            return Ok(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListExists(int id)
        {
            return db.List.Count(e => e.ListId == id) > 0;
        }
    }
}