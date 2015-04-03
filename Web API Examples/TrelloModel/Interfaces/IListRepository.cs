using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces
{
    public interface IListRepository : IRepository<List>
    {
        IEnumerable<List> GetListsOfBoard(int boardId);

        Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId);
    }
}