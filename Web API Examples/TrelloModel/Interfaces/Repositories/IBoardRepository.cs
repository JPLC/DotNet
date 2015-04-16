﻿namespace TrelloModel.Interfaces.Repositories
{
    public interface IBoardRepository : IRepository<Board>
    {
        bool HasRepeatedBoardName(string boardname);
    }
}