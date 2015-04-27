using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.MongoDB
{
    public class ListRepositoryMongoDB : IListRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<ListRepositoryMongoDB> BoardRepo = new Lazy<ListRepositoryMongoDB>(() => new ListRepositoryMongoDB());

        public static ListRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        public Task<IEnumerable<List>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<List>> GetAllPagingAsync<TKey>(Expression<Func<List, TKey>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
        {
            throw new NotImplementedException();
        }

        public Task<List> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List> FindByAsync(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<List>> FindAllByAsync(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List> AddAsync(List t)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(List t)
        {
            throw new NotImplementedException();
        }

        public Task EditRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<List>> GetListsOfBoardPagingAsync<TKey>(Expression<Func<List, TKey>> sorter, SortDirection direction, string searchString, int pagenumber,
            int pagesize, int boardid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<List>> GetListsOfBoardPagingAsync(Expression<Func<List, object>> sorter, SortDirection direction, string searchString, int pagenumber,
            int pagesize, int boardid)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetListBoardNameAsync(int boardId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountListsOfBoardAsync(int boardId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountConditionalListsOfBoardAsync(Expression<Func<List, bool>> predicate, int boardId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}