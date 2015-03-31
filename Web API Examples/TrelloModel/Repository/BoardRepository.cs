using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class BoardRepository : IRepository<Board>
    {
        #region Variables and Properties
        private static readonly Lazy<BoardRepository> BoardRepo = new Lazy<BoardRepository>(() => new BoardRepository());

        public static BoardRepository Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Constructor
        private BoardRepository() { }
        #endregion

        //TODO aplicar e melhorar expection handling
        #region Methods
        public IEnumerable<Board> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.ToList();
            }
        }

        public Board GetSingle(int id)
        {
            return GetAll().FirstOrDefault(b => b.BoardId == id);
        }

        public IEnumerable<Board> FindBy(Func<Board, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Add(board);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.AddRange(boards);
                db.SaveChanges();
            }
        }

        public void Delete(Board board)
        {
            Delete(board.BoardId);
        }

        private void Delete(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var board = db.Board.FirstOrDefault(b => b.BoardId == boardId);
                db.Board.Remove(board);
                db.SaveChanges();
            }
        }

        public void Edit(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion
    }
}