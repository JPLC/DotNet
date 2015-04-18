using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.MongoDB
{
    public class BoardRepositoryMongoDB : IRepository<Board>
    {
        #region Variables and Properties
        private static readonly Lazy<BoardRepositoryMongoDB> BoardRepo = new Lazy<BoardRepositoryMongoDB>(() => new BoardRepositoryMongoDB());

        public static BoardRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        public IEnumerable<Board> GetAll()
        {
            throw new NotImplementedException();
        }

        public Board GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Board FindBy(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Board> FindAllBy(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Board t)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Board> t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Board t)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Board> t)
        {
            throw new NotImplementedException();
        }

        public void Edit(Board t)
        {
            throw new NotImplementedException();
        }

        public void EditRange(IEnumerable<Board> t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool ValidId(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}