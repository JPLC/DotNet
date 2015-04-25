using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepository : IRepository<Board>
    {
        bool HasRepeatedBoardName(int boardid, string boardname);

        int CountConditional(Expression<Func<Board, bool>> predicate);

        IEnumerable<Board> GetAllPaging(Expression<Func<Board, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize);
    }
}
