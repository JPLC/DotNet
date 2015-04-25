using System;
using System.Linq.Expressions;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepository : IRepository<Board>
    {
        bool HasRepeatedBoardName(int boardid, string boardname);

        int CountConditional(Expression<Func<Board, bool>> predicate);
    }
}
