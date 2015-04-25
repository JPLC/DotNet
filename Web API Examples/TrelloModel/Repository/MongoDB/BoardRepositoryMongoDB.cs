using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.MongoDB
{
    public class BoardRepositoryMongoDB : IBoardRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<BoardRepositoryMongoDB> BoardRepo = new Lazy<BoardRepositoryMongoDB>(() => new BoardRepositoryMongoDB());

        public static BoardRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        public Task<IEnumerable<Board>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> GetAllPagingAsync(Expression<Func<Board, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
        {
            throw new NotImplementedException();
        }

        public Task<Board> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Board> FindByAsync(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> FindAllByAsync(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Board> AddAsync(Board t)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Board> t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Board t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<Board> t)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(Board t)
        {
            throw new NotImplementedException();
        }

        public Task EditRangeAsync(IEnumerable<Board> t)
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

        public Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountConditionalAsync(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> GetAllPaging(Expression<Func<Board, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}