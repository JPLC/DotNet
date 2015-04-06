using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Interfaces.Factories
{
    public interface ICardRepositoryFactory
    {
        IRepository<Card> GetCardRepositorySQL();
    }
}