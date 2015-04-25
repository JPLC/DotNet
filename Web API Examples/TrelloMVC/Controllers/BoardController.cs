using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrelloModel;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository.SQL;
using TrelloModel.Resources;
using TrelloMVC.ViewModels.BoardViewModels;
using TrelloMVC.ViewModels.Converters;

namespace TrelloMVC.Controllers
{
    /*[RequireHttps]*/
    [RoutePrefix("Board")]
    public class BoardController : Controller
    {
        #region Variables and Properties
        private static BoardRepositorySQL _br;
        private const int PageSize = 5;
        #endregion

        #region Constructors
        public BoardController(IBoardRepositoryFactory brf)
        {
            _br = (BoardRepositorySQL)brf.GetBoardRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: Board/All
        [HttpGet]
        [Route("All")]
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Discription" ? "discription" : "Discription";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            var elemc = _br.Count();
            ViewBag.ElemCount = elemc;
            ViewBag.CurrentFilter = searchString;
            ViewBag.PageCount = (int)Math.Ceiling((double) elemc/PageSize);
            var pageNumber = (page ?? 1);
            IEnumerable<BoardViewModel> boards = SortingFilteringPaging(sortOrder, searchString, pageNumber);            
            return View(new Tuple<IEnumerable<BoardViewModel>, int>(boards, pageNumber));
        }

        // GET: Board/Details/5
        [HttpGet]
        [Route("Details/{id:int}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Board board = await db.Board.FindAsync(id);
            Board board = _br.GetSingle(id.Value);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(VMConverters.ModelToViewModel(board));
        }

        // GET: Board/Create
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name, Discription")] BoardViewModel boardvm)
        {
            if (_br.HasRepeatedBoardName(boardvm.Id, boardvm.Name))
            {
                ModelState.AddModelError("Name", BoardResources.BoardNameAlreadyExists);
            }
            if (ModelState.IsValid)
            {
                _br.Add(VMConverters.ViewModelToModel(boardvm));
                return RedirectToAction("Index");
            }
            return View(boardvm);
        }

        // GET: Board/Edit/5
        [HttpGet]
        [Route("Edit/{id:int}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var board = _br.GetSingle(id.Value);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(VMConverters.ModelToViewModel(board));
        }

        // POST: Board/Edit/5
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include = "Name, Discription")] BoardViewModel boardvm)
        {
            if (!_br.ValidId(id))
            {
                return HttpNotFound();
            }
            boardvm.Id = id;
            if (_br.HasRepeatedBoardName(boardvm.Id, boardvm.Name))
            {
                ModelState.AddModelError("Name", BoardResources.BoardNameAlreadyExists);
            }
            if (!ModelState.IsValid)
                return View(boardvm);
            _br.Edit(VMConverters.ViewModelToModel(boardvm));
            return RedirectToAction("Index");
        }

        // GET: Board/Delete/5
        [HttpGet]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Board board = await db.Board.FindAsync(id);
            var board = _br.GetSingle(id.Value);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(VMConverters.ModelToViewModel(board));
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            /* Board board = await db.Board.FindAsync(id);
             db.Board.Remove(board);
             await db.SaveChangesAsync();*/
            _br.Delete(_br.GetSingle(id));
            return RedirectToAction("Index");
        }

        /*[HttpGet]
        [Route("CheckBoardName")]
        public JsonResult CheckBoardName(string Name)
        {
            var result = _br.HasRepeatedBoardName(id, Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        */
        #endregion

        #region Auxiliar Methods
        public  IEnumerable<BoardViewModel> SortingFilteringPaging(string sortOrder, string searchString, int pagenumber)
        {
            var boards = VMConverters.ModelsToViewModels(_br.GetAllPaging(searchString, pagenumber, PageSize));
            switch (sortOrder)
            {
                case "name_desc":
                    boards = boards.OrderByDescending(b => b.Name);
                    break;
                case "Discription":
                    boards = boards.OrderBy(b => b.Discription);
                    break;
                case "discription":
                    boards = boards.OrderByDescending(b => b.Discription);
                    break;
                default: // Name ascending 
                    boards = boards.OrderBy(b => b.Name);
                    break;
            }
            return boards;
        }
        #endregion
    }
}