using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository;
using TrelloModel.Repository.SQL;
using TrelloMVC.ViewModels;
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
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            var sortfiltaux = new CardSortFilter
            {
                CurrentSort = sortOrder,
                NameSortParm = string.IsNullOrEmpty(sortOrder) ? CardVMConstants.NameDesc : string.Empty,
                DiscriptionSortParm = sortOrder == CardVMConstants.DiscriptionAsc ? CardVMConstants.DiscriptionDesc : CardVMConstants.DiscriptionAsc,
                CDateSortParm = sortOrder == CardVMConstants.CDateAsc ? CardVMConstants.CDateDesc : CardVMConstants.CDateAsc,
                DueDateSortParm = sortOrder == CardVMConstants.DueDateAsc ? CardVMConstants.DueDateDesc : CardVMConstants.DueDateAsc,
                CixSortParm = sortOrder == CardVMConstants.CixAsc ? CardVMConstants.CixDesc : CardVMConstants.CixAsc,
                CurrentFilter = searchString,
            };

            var elemcount = !String.IsNullOrEmpty(searchString) ? _cr.CountConditionalCardsOfList(b => b.Name.Contains(searchString), boardid.Value) : _cr.CountCardsOfList(boardid.Value);
            var pageaux = new PaginationAux
            {
                ElementsCount = elemcount,
                PageCount = (int)Math.Ceiling((double)elemcount / PageSize),
                PageNumber = (page ?? 1),
                PageSize = PageSize
            };

            IEnumerable<CardViewModel> cards = SortingFilteringPaging(sortOrder, searchString, pageaux.PageNumber, listid.Value, listname);

            return View("Index", new Tuple<IEnumerable<CardViewModel>, PaginationAux, CardSortFilter>(cards, pageaux, sortfiltaux));
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
            IEnumerable<CardViewModel> cards;
            switch (sortOrder)
            {
                case CardVMConstants.NameDesc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Name, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.NameAsc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Name, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DiscriptionDesc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DiscriptionAsc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Discription, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CDateDesc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.CreationDate, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CDateAsc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.CreationDate, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DueDateDesc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.DueDate, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.DueDateAsc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.DueDate, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CixDesc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Descending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                case CardVMConstants.CixAsc:
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
                default: // Cix ascending 
                    cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfListPaging(e => e.Cix, SortDirection.Ascending, searchString, pagenumber, PageSize, listid), listname);
                    break;
            }
            return cards;
        }
        #endregion
    }
}