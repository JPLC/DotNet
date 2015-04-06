using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrelloModel;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository.SQL;

namespace TrelloMVC.Controllers
{
    public class CardController : Controller
    {
        private TrelloModelDBContainer db;// = new TrelloModelDBContainer();
        
        #region Variables
        private static CardRepositorySQL _cr;
        #endregion

        #region Action Methods
        public CardController(ICardRepositoryFactory listRepository)
        {
            _cr = (CardRepositorySQL) listRepository.GetCardRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: Card
        public async Task<ActionResult> Index()
        {
            var card = db.Card.Include(c => c.Board).Include(c => c.List);
            return View(await card.ToListAsync());
        }

        // GET: Card/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(card);
        }

        // GET: Card/Create
        public ActionResult Create()
        {
            ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name");
            ViewBag.ListId = new SelectList(db.List, "ListId", "Name");
            return View();
        }

        // POST: Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CardId,Cix,Name,Discription,CreationDate,DueDate,BoardId,ListId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Card.Add(card);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", card.BoardId);
            ViewBag.ListId = new SelectList(db.List, "ListId", "Name", card.ListId);
            return View(card);
        }

        // GET: Card/Edit/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CardId,Cix,Name,Discription,CreationDate,DueDate,BoardId,ListId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", card.BoardId);
            ViewBag.ListId = new SelectList(db.List, "ListId", "Name", card.ListId);
            return View(card);
        }

        // GET: Card/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(card);
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Card card = await db.Card.FindAsync(id);
            db.Card.Remove(card);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}