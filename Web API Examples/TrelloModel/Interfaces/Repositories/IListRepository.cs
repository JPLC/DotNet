using System.Collections.Generic;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        IEnumerable<List> GetListsOfBoard(int boardId);
    }
}