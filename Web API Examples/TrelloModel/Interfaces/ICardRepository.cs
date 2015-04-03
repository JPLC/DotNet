using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<Card> GetCardsOfBoard(int boardId);

        Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId);

        IEnumerable<Card> GetCardsOfList(int listId);

        Task<IEnumerable<Card>> GetCardsOfListAsync(int listId);
    }
}
