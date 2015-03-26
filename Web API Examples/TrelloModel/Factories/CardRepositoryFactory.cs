using TrelloModel.Interfaces;
using TrelloModel.Repository;

namespace TrelloModel.Factories
{
    public class CardRepositoryFactory : ICardRepositoryFactory
    {
        public IRepository<Card> GetCardRepository()
        {
            return new CardRepository();
        }
    }
}