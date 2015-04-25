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

        Task<IEnumerable<Card>> GetCardsOfBoardPagingAsync(Expression<Func<Card, object>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int boardid);

        Task<IEnumerable<Card>> GetCardsOfListAsync(int listId);

        Task<IEnumerable<Card>> GetCardsOfListPagingAsync(Expression<Func<Card, object>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int listid);

        Task<string> GetCardListNameAsync(int listId);
    }
}
