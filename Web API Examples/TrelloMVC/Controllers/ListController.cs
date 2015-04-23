using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using TrelloModel;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository.SQL;
using TrelloMVC.ViewModels.Converters;
using TrelloMVC.ViewModels.ListViewModels;

namespace TrelloMVC.Controllers
{
    [RoutePrefix("Board/{boardid:int}/List")]
    public class ListController : Controller
    {
        #region Variables
        private static ListRepositorySQL _lr;
        private const int PageSize = 5;
        #endregion

        #region Constructor
        public ListController(IListRepositoryFactory listRepository)
        {
            _lr = (ListRepositorySQL)listRepository.GetListRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: List
        /* public ActionResult Index()
         {
             var list = _lr.GetAll();
             return View(list);
         }*/
        [Route("All")]
        public ActionResult ListsOfBoard(int? boardid, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (boardid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var boardname = _lr.GetListBoardName(boardid.Value);
            var lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoard(boardid.Value), boardname);

            ViewBag.BoardId = boardid;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Lix" ? "lix" : "Lix";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                lists = lists.Where(b => b.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    lists = lists.OrderByDescending(b => b.Name);
                    break;
                case "Lix":
                    lists = lists.OrderBy(b => b.Lix);
                    break;
                case "lix":
                    lists = lists.OrderByDescending(b => b.Lix);
                    break;
                default: // Name ascending 
                    lists = lists.OrderBy(b => b.Name);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View("Index", (lists.ToPagedList(pageNumber, PageSize)));
        }

        // GET: List/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? boardid, int? id)
        {
            if (boardid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List list = db.List.Find(id);
            var list = _lr.GetSingle(id.Value);
            ViewBag.BoardId = boardid.Value;
            var boardname = _lr.GetListBoardName(boardid.Value);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(VMConverters.ModelToViewModel(list,boardname));
        }

        // GET: List/Create
        [Route("Create")]
        public ActionResult Create(int? boardid)
        {
            if (boardid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.BoardId = boardid;
            return View();
        }

        // POST: List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lix,Name")] ListViewModel listvm)
        {
            if (ModelState.IsValid)
            {
                var list = 
                _lr.Add(list);
                return RedirectToAction("Details", new { id = listvm.Id });
            }
            //ViewBag.BoardId = new SelectList(_lr.GetAll(), "BoardId", "Name", list.BoardId);
            return RedirectToAction("ListsOfBoard");
        }

        // GET: List/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? boardid, int? id)
        {
            if (boardid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List list = db.List.Find(id);
            var list = _lr.GetSingle(id.Value);
            if (list == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", list.BoardId);
            ViewBag.BoardId = list.BoardId;
            var boardname = _lr.GetListBoardName(boardid.Value);
            return View(VMConverters.ModelToViewModel(list, boardname));
        }

        // POST: List/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? boardid, int? id, [Bind(Include = "Lix,Name")] ListViewModel listvm)
        {
            if (boardid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                //list.BoardId = _lr.GetSingle(list.ListId).BoardId;
                var list = VMConverters.ViewModelToModel(listvm, boardid.Value);
                _lr.Edit(list);
                return RedirectToAction("Details", new { id = list.ListId });
            }
            //ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", list.BoardId);
            ViewBag.BoardId = boardid.Value;
            return View(listvm);
        }

        // GET: List/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = _lr.GetSingle(id.Value);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: List/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var list = _lr.GetSingle(id);
            _lr.Delete(list);
            return RedirectToAction("ListsOfBoard", new { boardid = list.BoardId });
        }
        #endregion
    }
}