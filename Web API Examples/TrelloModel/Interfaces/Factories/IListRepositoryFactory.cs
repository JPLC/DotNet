using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Interfaces.Factories
{
    public interface IListRepositoryFactory
    {
        IRepository<List> GetListRepositorySQL();
    }
}