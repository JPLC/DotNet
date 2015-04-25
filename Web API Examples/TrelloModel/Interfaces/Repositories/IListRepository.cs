using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        IEnumerable<List> GetListsOfBoard(int boardId);

        IEnumerable<List> GetListsOfBoardPaging(Expression<Func<List, object>> sorter, SortDirection direction,
            string searchString, int pagenumber, int pagesize, int boardid);

        string GetListBoardName(int boardId);

        int CountListsOfBoard(int boardId);

        int CountConditionalListsOfBoard(Expression<Func<List, bool>> predicate, int boardId);
    }
}