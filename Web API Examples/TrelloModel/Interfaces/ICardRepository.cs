using System.Collections.Generic;

namespace TrelloModel.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<Card> GetCardsOfBoard(int boardId);
        IEnumerable<Card> GetCardsOfList(int listId);
    }
}
