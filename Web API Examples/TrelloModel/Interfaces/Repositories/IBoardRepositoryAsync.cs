using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepositoryAsync : IRepositoryAsync<Board>
    {
        Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname);
    }
}