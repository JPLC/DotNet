using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloModel;
using TrelloMVC.ViewModels.BoardViewModels;

namespace TrelloMVC.ViewModelsConverters
{
    public static class VMConverters
    {
        #region BoardsViewModel
        public static BoardsViewModel ModelToViewModel(Board board)
        {
            return new BoardsViewModel {Id = board.BoardId, Name = board.Name, Discription = board.Discription};
        }

        public static IEnumerable<BoardsViewModel> ModelsToViewModels(IEnumerable<Board> boards)
        {
            return boards.Select(board => ModelToViewModel(board)).ToList();
        }
        
        public static Board ViewModelToModel(BoardsViewModel boardvm)
        {
            return new Board { BoardId = boardvm.Id, Name = boardvm.Name, Discription = boardvm.Discription };
        }

        public static IEnumerable<Board> ViewModelsToModels(IEnumerable<BoardsViewModel> boardsvms)
        {
            return boardsvms.Select(boardvm => ViewModelToModel(boardvm)).ToList();
        }
        #endregion
    }
}