using TrelloModel.Interfaces;
using TrelloModel.Interfaces.Factories;

namespace TrelloModel.Factories
{
    public class RepositoryConcreteFactory : RepositoryAbstractFactory
    {
        public override IBoardRepositoryFactory GetBoardFactory()
        {
            return new BoardRepositoryFactory();
        }

        public override IListRepositoryFactory GetListFactory()
        {
            return new ListRepositoryFactory();
        }

        public override ICardRepositoryFactory GetCardFactory()
        {
            return new CardRepositoryFactory();
        }
    }
}