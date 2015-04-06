using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface ICardRepositoryAsync : ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId);

        Task<IEnumerable<Card>> GetCardsOfListAsync(int listId);
    }
}
