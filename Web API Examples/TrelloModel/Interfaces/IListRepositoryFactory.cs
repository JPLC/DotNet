namespace TrelloModel.Interfaces
{
    public interface IListRepositoryFactory
    {
        IRepository<List> GetListRepository();
    }
}