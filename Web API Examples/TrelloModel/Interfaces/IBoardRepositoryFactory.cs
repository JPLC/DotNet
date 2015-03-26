namespace TrelloModel.Interfaces
{
    public interface IBoardRepositoryFactory
    {
        IRepository<Board> GetBoardRepository();
    }
}