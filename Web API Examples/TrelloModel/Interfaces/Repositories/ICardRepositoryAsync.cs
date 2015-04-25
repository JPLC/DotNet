using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface ICardRepositoryAsync : IRepositoryAsync<Card>
    {
        Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId);

        Task<IEnumerable<Card>> GetCardsOfListAsync(int listId);

        Task<string> GetCardListNameAsync(int listId);
    }
}
