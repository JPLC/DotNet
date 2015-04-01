using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TrelloModel;
using TrelloModel.Interfaces;
using TrelloModel.Repository;

namespace TrelloWebAPI.Controllers
{
    public class BoardController : ApiController
    {
        private static BoardRepository _br;
        public BoardController(IBoardRepositoryFactory boardRepository)
        {
            _br = (BoardRepository)boardRepository.GetBoardRepository();
        }

        // GET: api/Board
        public IHttpActionResult GetAllBoards()
        {
            return Ok(_br.GetAll());
        }

        // GET: api/Board/5
        [ResponseType(typeof(Board))]
        public IHttpActionResult GetOneBoard(int id)
        {
            var board = _br.GetSingle(id);
            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        // POST: api/Board
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Board/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Board/5
        public void Delete(int id)
        {
        }
    }
}
