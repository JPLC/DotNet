using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.MongoDB
{
    public class ListRepositoryMongoDB : IListRepository
    {
        #region Variables and Properties
        private static readonly Lazy<ListRepositoryMongoDB> BoardRepo = new Lazy<ListRepositoryMongoDB>(() => new ListRepositoryMongoDB());

        public static ListRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        public IEnumerable<List> GetAll()
        {
            throw new NotImplementedException();
        }

        public List GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public List FindBy(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List> FindAllBy(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(List t)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public void Delete(List t)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public void Edit(List t)
        {
            throw new NotImplementedException();
        }

        public void EditRange(IEnumerable<List> t)
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

        public IEnumerable<List> GetListsOfBoard(int boardId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}