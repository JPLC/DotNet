using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IListRepositoryAsync : IRepositoryAsync<List>
    {
        Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId);

        Task<string> GetListBoardNameAsync(int boardId);
    }
}