using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TrelloModel;
using TrelloModel.Interfaces;
using TrelloModel.Repository;

namespace TrelloWebAPI.Controllers
{
    public class ListController : ApiController
    {
        private static ListRepository _lr;
        public ListController(IListRepositoryFactory listRepository)
        {
            _lr = (ListRepository)listRepository.GetListRepository();
        }


        // GET: api/List
        /*public IQueryable<List> GetList()
        {
            return _lr.List;
        }*/

        // GET: api/List/5
        [ResponseType(typeof(List))]
        public /*async Task<*/IHttpActionResult/*>*/ GetList(int id)
        {
            List list = _lr.GetSingle(id);// await _lr.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // PUT: api/List/5
        [ResponseType(typeof(void))]
        public /*async Task<*/IHttpActionResult/*>*/ PutList(int id, List list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != list.ListId)
            {
                return BadRequest();
            }

            //_lr.Entry(list).State = EntityState.Modified;
            _lr.Edit(list);
           /* try
            {
                await _lr.SaveChangesAsync();
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
            }*/

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/List
        [ResponseType(typeof(List))]
        public /*async Task<*/IHttpActionResult/*>*/ PostList(List list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_lr.List.Add(list);
            //await _lr.SaveChangesAsync();
            _lr.Add(list);
            return CreatedAtRoute("DefaultApi", new { id = list.ListId }, list);
        }

        // DELETE: api/List/5
        [ResponseType(typeof(List))]
        public /*async Task<*/IHttpActionResult/*>*/ DeleteList(int id)
        {
            List list = _lr.GetSingle(id); //await _lr.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            //_lr.List.Remove(list);
            //await _lr.SaveChangesAsync();
            _lr.Delete(list);
            return Ok(list);
        }

        protected override void Dispose(bool disposing)
        {
            /*if (disposing)
            {
                _lr.Dispose();
            }
            base.Dispose(disposing);*/
        }

       /* private bool ListExists(int id)
        {
            return _lr.List.Count(e => e.ListId == id) > 0;
        }*/
    }
}