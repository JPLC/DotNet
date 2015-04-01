using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        #region Variables
        private static BoardRepository _br;
        #endregion

        #region Constructor
        public BoardController(IBoardRepositoryFactory boardRepository)
        {
            _br = (BoardRepository)boardRepository.GetBoardRepository();
        }
        #endregion

        #region ActionMethods
        // GET: TrelloAPI/Board
        [Route("TrelloAPI/Boards")]
        [HttpGet]
        public IHttpActionResult GetAllBoards()
        {
            return Ok(_br.GetAll());
        }

        // GET: TrelloAPI/Board/5
        [Route("TrelloAPI/Board/{id:int}")]
        [HttpGet]
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

        // POST: TrelloAPI/Board
        [Route("TrelloAPI/Board")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Board board)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _br.Add(board);
            return CreatedAtRoute("GetOneBoard", new { id = board.BoardId }, board);
        }

        // PUT: TrelloAPI/Board/5
        [Route("TrelloAPI/Board/{id:int}")]
        [HttpPut]
        public IHttpActionResult Put(int id, Board board)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != board.BoardId)
            {
                return BadRequest();
            }
            try
            {
                _br.Edit(board);
            }
            catch (DbUpdateConcurrencyException)
            {
                /*
                if (!ListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: TrelloAPI/Board/5
        [Route("TrelloAPI/Board/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Board board = _br.GetSingle(id);
            if (board == null)
            {
                return NotFound();
            }
            _br.Delete(board);
            return Ok(board);
        }
        #endregion
    }
}