using TrelloModel.Interfaces;
using TrelloModel.Repository;

namespace TrelloModel.Factories
{
    public class ListRepositoryFactory : IListRepositoryFactory
    {
        public IRepository<List> GetListRepository()
        {
            return ListRepository.Instance;
        }
    }
}