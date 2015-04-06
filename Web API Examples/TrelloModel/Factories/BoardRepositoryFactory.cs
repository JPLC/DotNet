using TrelloModel.Interfaces.Factories;
using TrelloModel.Interfaces.Repositories;
using TrelloModel.Repository.MongoDB;
using TrelloModel.Repository.SQL;

namespace TrelloModel.Factories
{
    public class BoardRepositoryFactory : IBoardRepositoryFactory
    {
        public IRepository<Board> GetBoardRepositorySQL()
        {
            return BoardRepositorySQL.Instance;
        }

        public IRepository<Board> GetBoardRepositoryMongoDB()
        {
            return BoardRepositoryMongoDB.Instance;
        }
    }
}