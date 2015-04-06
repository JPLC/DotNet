using TrelloModel.Interfaces;
using TrelloModel.Interfaces.Factories;

namespace TrelloModel.Factories
{
    public abstract class RepositoryAbstractFactory
    {
        public abstract IBoardRepositoryFactory GetBoardFactory();
        public abstract IListRepositoryFactory GetListFactory();
        public abstract ICardRepositoryFactory GetCardFactory();
    }
}