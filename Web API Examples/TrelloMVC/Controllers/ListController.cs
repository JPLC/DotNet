using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository;
using TrelloModel.Repository.SQL;
using TrelloMVC.ViewModels;
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
            ViewBag.BoardId = boardid.Value;
            var boardname = _lr.GetListBoardName(boardid.Value);
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            var sortfiltaux = new ListSortFilter
            {
                CurrentSort = sortOrder,
                NameSortParm = string.IsNullOrEmpty(sortOrder) ? ListVMConstants.NameDesc : string.Empty,
                LixSortParm = sortOrder == ListVMConstants.LixAsc ? ListVMConstants.LixDesc : ListVMConstants.LixAsc,
                CurrentFilter = searchString,
            };

            var elemcount = !String.IsNullOrEmpty(searchString) ? _lr.CountConditionalListsOfBoard(b => b.Name.Contains(searchString), boardid.Value) : _lr.CountListsOfBoard(boardid.Value);
            var pageaux = new PaginationAux
            {
                ElementsCount = elemcount,
                PageCount = (int)Math.Ceiling((double)elemcount / PageSize),
                PageNumber = (page ?? 1),
                PageSize = PageSize
            };

            IEnumerable<ListViewModel> lists = SortingFilteringPaging(sortOrder, searchString, pageaux.PageNumber, boardid.Value, boardname);
            return View("Index", new Tuple<IEnumerable<ListViewModel>, PaginationAux, ListSortFilter>(lists, pageaux, sortfiltaux));
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
            if (list == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = boardid.Value;
            var boardname = _lr.GetListBoardName(boardid.Value);
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
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? boardid,[Bind(Include = "Lix,Name")] ListViewModel listvm)
        {
            if (boardid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var list = VMConverters.ViewModelToModel(listvm, boardid.Value);
                _lr.Add(list);
                return RedirectToAction("ListsOfBoard");
            }
            return View();
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
        public ActionResult Delete(int? boardid, int? id)
        {
            if (boardid==null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = _lr.GetSingle(id.Value);
            if (list == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = boardid.Value;
            var boardname = _lr.GetListBoardName(boardid.Value);
            return View(VMConverters.ModelToViewModel(list, boardname));
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

        #region Auxiliar Methods
        public IEnumerable<ListViewModel> SortingFilteringPaging(string sortOrder, string searchString, int pagenumber, int boardid, string boardname)
        {
            IEnumerable<ListViewModel> lists;
            switch (sortOrder)
            {
                case ListVMConstants.NameDesc:
                    lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoardPaging(e => e.Name, SortDirection.Descending, searchString, pagenumber, PageSize, boardid), boardname);
                    break;
                case ListVMConstants.NameAsc:
                    lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoardPaging(e => e.Name, SortDirection.Ascending, searchString, pagenumber, PageSize, boardid), boardname);
                    break;
                case ListVMConstants.LixDesc:
                    lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoardPaging(e => e.Lix, SortDirection.Descending, searchString, pagenumber, PageSize, boardid), boardname);
                    break;
                case ListVMConstants.LixAsc:
                    lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoardPaging(e => e.Lix, SortDirection.Ascending, searchString, pagenumber, PageSize, boardid), boardname);
                    break;
                default: // Lix ascending 
                    lists = VMConverters.ModelsToViewModels(_lr.GetListsOfBoardPaging(e => e.Lix, SortDirection.Ascending, searchString, pagenumber, PageSize, boardid), boardname);
                    break;
            }
            return lists;
        }
        #endregion
    }
}