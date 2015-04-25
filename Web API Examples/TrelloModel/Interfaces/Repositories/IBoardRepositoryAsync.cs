using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepositoryAsync : IRepositoryAsync<Board>
    {
        Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname);

        Task<int> CountConditionalAsync(Expression<Func<Board, bool>> predicate);
    }
}