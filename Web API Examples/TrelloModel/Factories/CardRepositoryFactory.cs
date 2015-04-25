using System;
using TrelloModel.Interfaces.Factories;
using TrelloModel.Interfaces.Repositories;
using TrelloModel.Repository.MongoDB;
using TrelloModel.Repository.SQL;

namespace TrelloModel.Factories
{
    public class CardRepositoryFactory : ICardRepositoryFactory
    {
        public IRepository<Card> GetCardRepositorySQL()
        {
            return CardRepositorySQL.Instance;
        }

        public IRepository<Card> GetCardRepositoryMongoDB()
        {
            throw new NotImplementedException();
        }
    }
}