using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public void Add(Board entity)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //try
                //{
                    db.Board.Add(entity);
                    db.SaveChanges();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                //}
            }
        }

        public void Delete(Board entity)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Remove(entity);
                db.SaveChanges();
            }
        }

        public void Edit(Board entity)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion
    }
}