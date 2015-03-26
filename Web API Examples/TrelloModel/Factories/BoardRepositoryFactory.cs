using TrelloModel.Interfaces;
using TrelloModel.Repository;

namespace TrelloModel.Factories
{
    public class BoardRepositoryFactory : IBoardRepositoryFactory
    {
        public IRepository<Board> GetBoardRepository()
        {
            return new BoardRepository();
        }
    }
}