using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IListRepositoryAsync : IListRepository
    {
        Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId);
    }
}