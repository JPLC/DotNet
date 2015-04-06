using TrelloModel.Interfaces.Factories;
using TrelloModel.Interfaces.Repositories;
using TrelloModel.Repository.MongoDB;
using TrelloModel.Repository.SQL;

namespace TrelloModel.Factories
{
    public class ListRepositoryFactory : IListRepositoryFactory
    {
        public IRepository<List> GetListRepositorySQL()
        {
            return ListRepositorySQL.Instance;
        }
        public IRepository<List> GetListRepositoryMongoDB()
        {
            return ListRepositoryMongoDB.Instance;
        }
    }
}