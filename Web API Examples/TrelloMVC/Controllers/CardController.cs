using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TrelloModel;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository.SQL;
using TrelloMVC.ViewModels.CardViewModels;
using TrelloMVC.ViewModels.Converters;

namespace TrelloMVC.Controllers
{
    [RoutePrefix("Board/{boardid:int}/List/{listid:int}/Card")]
    public class CardController : Controller
    {
        private TrelloModelDBContainer db;// = new TrelloModelDBContainer();
        
        #region Variables
        private static CardRepositorySQL _cr;
        private const int PageSize = 5;
        #endregion

        #region Constructor
        public CardController(ICardRepositoryFactory listRepository)
        {
            _cr = (CardRepositorySQL) listRepository.GetCardRepositorySQL();
        }
        #endregion

        #region Action Methods

        // GET: Card
        [Route("All")]
        /*TODO Finishing pagination*/
        public async Task<ActionResult> CardsOfList(int? boardid, int? listid, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (boardid == null || listid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listname = _cr.GetCardListName(listid.Value);
            var cards = VMConverters.ModelsToViewModels(_cr.GetCardsOfList(listid.Value), listname);
            ViewBag.BoardId = boardid.Value;
            ViewBag.ListId = listid.Value;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Cix" ? "cix" : "Cix";

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
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Card.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", card.BoardId);
            ViewBag.ListId = new SelectList(db.List, "ListId", "Name", card.ListId);
            return View(card);
        }

        // POST: Card/Edit/5
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CardId,Cix,Name,Discription,CreationDate,DueDate,BoardId,ListId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CardsOfList");
            }
            ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", card.BoardId);
            ViewBag.ListId = new SelectList(db.List, "ListId", "Name", card.ListId);
            return View(card);
        }

        // GET: Card/Delete/5
        [Route("Delete/{id:int}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var card = _cr.GetSingle(id.Value);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
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
    }
}