using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IListRepositoryAsync : IRepositoryAsync<List>
    {
        Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId);

        Task<IEnumerable<List>> GetListsOfBoardPagingAsync<TKey>(Expression<Func<List, TKey>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int boardid);

        Task<string> GetListBoardNameAsync(int boardId);

        Task<int> CountListsOfBoardAsync(int boardId);

        Task<int> CountConditionalListsOfBoardAsync(Expression<Func<List, bool>> predicate, int boardId);
    }
}