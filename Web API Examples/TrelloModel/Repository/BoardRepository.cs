using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
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
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Board.ToList();
            }
        }

        public Board GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Board.FirstOrDefault(b => b.BoardId == id);
            }
        }

        public Board FindBy(Func<Board, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Board.FirstOrDefault(predicate);
            }
        }

        public IEnumerable<Board> FindAllBy(Func<Board, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Board.Where(predicate).ToList();
            }
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
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Where(b => b.BoardId == board.BoardId).Delete();
                db.SaveChanges();
            }
        }

        //TODO Acabar o problema na condição que da exception
        public void DeleteRange(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var aux = db.Board.Where(p => boards.All(p2 => p2.BoardId == p.BoardId));
                db.Board.RemoveRange(aux);
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

        public void EditRange(IEnumerable<Board> boards)
        {
            foreach (var board in boards)
            {
                Edit(board);
            }
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Board.Count();
            }
        }
        #endregion
    }
}