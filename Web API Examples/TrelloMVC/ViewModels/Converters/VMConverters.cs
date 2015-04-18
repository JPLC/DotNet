using System.Collections.Generic;
using System.Linq;
using TrelloModel;
using TrelloMVC.ViewModels.BoardViewModels;

namespace TrelloMVC.ViewModels.Converters
{
    public static class VMConverters
    {
        #region BoardViewModel
        public static BoardViewModel ModelToViewModel(Board board)
        {
            return new BoardViewModel {Id = board.BoardId, Name = board.Name, Discription = board.Discription};
        }

        public static IEnumerable<BoardViewModel> ModelsToViewModels(IEnumerable<Board> boards)
        {
            return boards.Select(board => ModelToViewModel(board)).ToList();
        }
        
        public static Board ViewModelToModel(BoardViewModel boardvm)
        {
            return new Board { BoardId = boardvm.Id, Name = boardvm.Name, Discription = boardvm.Discription };
        }

        public static IEnumerable<Board> ViewModelsToModels(IEnumerable<BoardViewModel> boardsvms)
        {
            return boardsvms.Select(boardvm => ViewModelToModel(boardvm)).ToList();
        }
        #endregion
    }
}