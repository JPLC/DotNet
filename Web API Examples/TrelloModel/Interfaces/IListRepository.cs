using System.Collections.Generic;

namespace TrelloModel.Interfaces
{
    public interface IListRepository : IRepository<List>
    {
        IEnumerable<List> GetListsOfBoard(int boardId);
    }
}