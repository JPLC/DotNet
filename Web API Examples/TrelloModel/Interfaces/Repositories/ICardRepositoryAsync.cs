using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface ICardRepositoryAsync : IRepositoryAsync<Card>
    {
        Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId);

        Task<IEnumerable<Card>> GetCardsOfBoardPagingAsync<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int boardid);

        Task<IEnumerable<Card>> GetCardsOfListAsync(int listId);

        Task<IEnumerable<Card>> GetCardsOfListPagingAsync<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int listid);

        Task<string> GetCardListNameAsync(int listId);

        Task<int> CountCardsOfBoardAsyn(int boardId);

        Task<int> CountConditionalCardsOfBoardAsyn(Expression<Func<Card, bool>> predicate, int boardId);

        Task<int> CountCardsOfListAsyn(int listid);

        Task<int> CountConditionalCardsOfListAsync(Expression<Func<Card, bool>> predicate, int listid);
    }
}
