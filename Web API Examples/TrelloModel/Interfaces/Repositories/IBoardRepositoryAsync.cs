using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepositoryAsync : IRepositoryAsync<Board>
    {
        Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname);

        Task<int> CountConditionalAsync(Expression<Func<Board, bool>> predicate);

        Task<IEnumerable<Board>> GetAllPaging(Expression<Func<Board, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize);
    }
}