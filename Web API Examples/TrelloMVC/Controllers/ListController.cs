using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrelloModel;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Repository.SQL;

namespace TrelloMVC.Controllers
{
    public class ListController : Controller
    {
        #region Variables
        private static ListRepositorySQL _lr;
        #endregion

        #region Action Methods
        public ListController(IListRepositoryFactory listRepository)
        {
            _lr = (ListRepositorySQL) listRepository.GetListRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: List
        public ActionResult Index()
        {
            //var list = db.List.Include(l => l.Board);
            var list = _lr.GetAll();
            return View(list);
        }

        public ActionResult ListsOfBoard(int? boardid)
        {
            if (boardid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = _lr.FindAllBy(l => l.BoardId == boardid);
            return View("Index", list);
        }

        // GET: List/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List list = db.List.Find(id);
            var list = _lr.GetSingle(id.Value);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // GET: List/Create
        public ActionResult Create()
        {
            //ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name");
            return View();
        }

        // POST: List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListId,Lix,Name,BoardId")] List list)
        {
            if (ModelState.IsValid)
            {
                /*db.List.Add(list);
                db.SaveChanges();*/
                _lr.Add(list);
                return RedirectToAction("Index");
            }
            //ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", list.BoardId);
            ViewBag.BoardId = new SelectList(_lr.GetAll(), "BoardId", "Name", list.BoardId);
            return View(list);
        }

        // GET: List/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
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
            ViewBag.BoardId = new SelectList(_lr.GetAll(), "BoardId", "Name", list.BoardId);
            return View(list);
        }

        // POST: List/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListId,Lix,Name,BoardId")] List list)
        {
            if (ModelState.IsValid)
            {
                /*
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();*/
                _lr.Edit(list);
                return RedirectToAction("Index");
            }
            //ViewBag.BoardId = new SelectList(db.Board, "BoardId", "Name", list.BoardId);
            ViewBag.BoardId = new SelectList(_lr.GetAll(), "BoardId", "Name", list.BoardId);
            return View(list);
        }

        // GET: List/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _lr.Delete(_lr.GetSingle(id));
            return RedirectToAction("Index");
        }
        #endregion
    }
}