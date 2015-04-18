using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepositoryAsync : IBoardRepository
    {
        Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname);
    }
}