using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloModel;
using TrelloMVC.ViewModels.BoardViewModels;

namespace TrelloMVC.ViewModelsConverters
{
    /*TODO FInish view model converters*/
    public static class BoardViewModelConverter
    {

        public static BoardsViewModel ModelToViewModel(Board board)
        {
            return new BoardsViewModel{ Id = board.BoardId, Name = board.Name, Discription = board.Discription};
        }

        public static IEnumerable<BoardsViewModel> ModelsToViewModels(IEnumerable<Board> boards)
        {
            return boards.Select(board => new BoardsViewModel {Id = board.BoardId, Name = board.Name, Discription = board.Discription}).ToList();
        }

       /* public static Board ModelToViewModel(BoardsViewModel boardsViewModel)
        {
        }*/
    }
}