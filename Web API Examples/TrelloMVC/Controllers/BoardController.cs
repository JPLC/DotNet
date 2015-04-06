﻿using System;
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
    public class BoardController : Controller
    {
        #region Variables
        private static BoardRepositorySQL _br;
        #endregion

        #region Action Methods
        public BoardController(IBoardRepositoryFactory brf)
        {
            _br = (BoardRepositorySQL) brf.GetBoardRepositorySQL();
        }
        #endregion

        #region Action Methods
        // GET: Board
        public async Task<ActionResult> Index()
        {
            return View(_br.GetAll());
            //return View(await db.Board.ToListAsync());
        }

        // GET: Board/Details/5
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
            return View(board);
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BoardId,Name,Discription")] Board board)
        {
            if (ModelState.IsValid)
            {
                //db.Board.Add(board);
                //await db.SaveChangesAsync();
                _br.Add(board);
                return RedirectToAction("Index");
            }

            return View(board);
        }

        // GET: Board/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Board board = await db.Board.FindAsync(id);
            var board = _br.GetSingle(id.Value);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BoardId,Name,Discription")] Board board)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(board).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                _br.Edit(board);
                return RedirectToAction("Index");
            }
            return View(board);
        }

        // GET: Board/Delete/5
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
            return View(board);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           /* Board board = await db.Board.FindAsync(id);
            db.Board.Remove(board);
            await db.SaveChangesAsync();*/         
            _br.Delete(_br.GetSingle(id));
            return RedirectToAction("Index");
        }

        #endregion
    }
}