using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Interfaces.Factories
{
    public interface IBoardRepositoryFactory
    {
        IRepository<Board> GetBoardRepositorySQL();
    }
}