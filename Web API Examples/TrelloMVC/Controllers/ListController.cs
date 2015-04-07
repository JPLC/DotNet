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
            _lr = (ListRepositorySQL)listRepository.GetListRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: List
        public ActionResult Index()
        {
            var list = _lr.GetAll();
            return View(list);
        }

        public ActionResult ListsOfBoard(int? boardid)
        {
            if (boardid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = _lr.GetListsOfBoard(boardid.Value).OrderBy(l=>l.Lix);
            ViewBag.BoardId = boardid;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListId,Lix,Name,BoardId")] List list)
        {
            if (ModelState.IsValid)
            {
                _lr.Add(list);
                return RedirectToAction("Details", new { id = list.ListId });
            }
            //ViewBag.BoardId = new SelectList(_lr.GetAll(), "BoardId", "Name", list.BoardId);
            return RedirectToAction("Index");
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
            ViewBag.BoardId = list.BoardId;
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
                list.BoardId = _lr.GetSingle(list.ListId).BoardId;
                _lr.Edit(list);
                return RedirectToAction("Details", new {id = list.ListId});
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
            var list = _lr.GetSingle(id);
            _lr.Delete(list);
            return RedirectToAction("ListsOfBoard", new {boardid=list.BoardId});
        }
        #endregion
    }
}