namespace TrelloModel.Interfaces
{
    public interface ICardRepositoryFactory
    {
        IRepository<Card> GetCardRepository();
    }
}