using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository;
using TrelloModel.Repository.SQL;
using TrelloMVC.ViewModels.CardViewModels;
using TrelloMVC.ViewModels.Converters;

namespace TrelloMVC.Controllers
{
    [RoutePrefix("Board/{boardid:int}/List/{listid:int}/Card")]
    public class CardController : Controller
    {
        #region Variables
        private static CardRepositorySQL _cr;
        private const int PageSize = 5;
        #endregion

        #region Constructor
        public CardController(ICardRepositoryFactory listRepository)
        {
            _cr = (CardRepositorySQL)listRepository.GetCardRepositorySQL();
        }
        #endregion

        #region Action Methods

        // GET: Card
        /*TODO por a paginação mum metodo*/
        [Route("All")]
        public async Task<ActionResult> CardsOfList(int? boardid, int? listid, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (boardid == null || listid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listname = _cr.GetCardListName(listid.Value);
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;

            var cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfList(listid.Value), listname);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CixSortParm = sortOrder == "Cix" ? "cix" : "Cix";
            ViewBag.CDateSortParm = sortOrder == "CDate" ? "cDate" : "CDate";
            ViewBag.DueDateSortParm = sortOrder == "DueDate" ? "dueDate" : "DueDate";

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
                cards = cards.Where(b => b.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cards = cards.OrderByDescending(b => b.Name);
                    break;
                case "Cix":
                    cards = cards.OrderBy(b => b.Cix);
                    break;
                case "cix":
                    cards = cards.OrderByDescending(b => b.Cix);
                    break;

                case "CDate":
                    cards = cards.OrderBy(b => b.CreationDate);
                    break;
                case "cDate":
                    cards = cards.OrderByDescending(b => b.CreationDate);
                    break;
                case "DueDate":
                    cards = cards.OrderBy(b => b.DueDate);
                    break;
                case "dueDate":
                    cards = cards.OrderByDescending(b => b.DueDate);
                    break;

                default: // Name ascending 
                    cards = cards.OrderBy(b => b.Name);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View("Index", (cards.ToPagedList(pageNumber, PageSize)));
        }

        // GET: Card/Details/5
        [Route("Details/{id:int}")]
        public async Task<ActionResult> Details(int? boardid, int? listid, int? id)
        {
            if (boardid == null || listid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var card = _cr.GetSingle(id.Value);
            if (card == null)
            {
                return HttpNotFound();
            }
            var listname = _cr.GetCardListName(listid.Value);
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            return View(VMConverters.ModelToViewModel(card, listname));
        }

        // GET: Card/Create
        [Route("Create")]
        public ActionResult Create(int? boardid, int? listid)
        {
            if (boardid == null || listid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            return View();
        }

        // POST: Card/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int? boardid, int? listid, [Bind(Include = "Name,Discription,DueDate")] CardViewModel cardvm)
        {
            if (boardid == null || listid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                _cr.Add(VMConverters.ViewModelToModel(cardvm, boardid.Value, listid.Value));
                return RedirectToAction("CardsOfList");
            }

            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            return View(cardvm);
        }

        // GET: Card/Edit/5
        [Route("Edit/{id:int}")]
        public async Task<ActionResult> Edit(int? boardid, int? listid, int? id)
        {
            if (boardid == null || listid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var card = _cr.GetSingle(id.Value);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            var listname = _cr.GetCardListName(listid.Value);
            return View(VMConverters.ModelToViewModel(card, listname));
        }

        // POST: Card/Edit/5
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? boardid, int? listid, int? id, [Bind(Include = "Cix,Name,Discription,DueDate")] CardViewModel cardvm)
        {
            if (boardid == null || listid == null || id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                cardvm.Id = id.Value;
                _cr.Edit(VMConverters.ViewModelToModel(cardvm, boardid.Value, listid.Value));
                return RedirectToAction("CardsOfList");
            }
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            return View(cardvm);
        }

        // GET: Card/Delete/5
        [Route("Delete/{id:int}")]
        public async Task<ActionResult> Delete(int? boardid, int? listid, int? id)
        {
            if (boardid == null || listid == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var card = _cr.GetSingle(id.Value);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            var listname = _cr.GetCardListName(listid.Value);
            return View(VMConverters.ModelToViewModel(card, listname));
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            _cr.Delete(_cr.GetSingle(id));
            return RedirectToAction("CardsOfList");
        }
        #endregion

        #region Auxiliar Methods
        public IEnumerable<CardViewModel> SortingFilteringPaging(string sortOrder, string searchString, int pagenumber, int listid, string listname)
        {
            IEnumerable<CardViewModel> lists;
            switch (sortOrder)
            {
                case CardVMConstants.NameDesc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Name, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.NameAsc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Name, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DiscriptionDesc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DiscriptionAsc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CDateDesc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CDateAsc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DueDateDesc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DueDateAsc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CixDesc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CixAsc:
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                default: // Cix ascending 
                    lists = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
            }
            return lists;
        }
        #endregion
    }
}