using System.Collections.Generic;
using System.Linq;
using TrelloModel;
using TrelloMVC.ViewModels.BoardViewModels;
using TrelloMVC.ViewModels.ListViewModels;
using TrelloMVC.ViewModels.CardViewModels;

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

        #region ListViewModel
        public static ListViewModel ModelToViewModel(List list, string boardname)
        {   
            return new ListViewModel { Id = list.BoardId, Name = list.Name, Lix = list.Lix, BoardName = boardname};
        }

        public static IEnumerable<ListViewModel> ModelsToViewModels(IEnumerable<List> lists, string boardname)
        {
            return lists.Select(board => ModelToViewModel(board, boardname)).ToList();
        }

        public static List ViewModelToModel(ListViewModel listvm, int boardid)
        {
            return new List { ListId= listvm.Id, Name = listvm.Name, Lix = listvm.Lix, BoardId = boardid};
        }

        public static IEnumerable<List> ViewModelsToModels(IEnumerable<ListViewModel> listvms, int boardid)
        {
            return listvms.Select(boardvm => ViewModelToModel(boardvm, boardid)).ToList();
        }
        #endregion

        #region CardViewModel
        public static CardViewModel ModelToViewModel(Card card, string listname)
        {
            return new CardViewModel { Id = card.CardId, Name = card.Name, Cix = card.Cix, Discription=card.Discription,
                                       CreationDate = card.CreationDate,  DueDate = card.DueDate, ListName = listname };
        }

        public static IEnumerable<CardViewModel> ModelsToViewModels(IEnumerable<Card> cards, string listname)
        {
            return cards.Select(board => ModelToViewModel(board, listname)).ToList();
        }

        public static Card ViewModelToModel(CardViewModel cardvm, int boardid, int listid)
        {
            return new Card { CardId = cardvm.Id, Name = cardvm.Name, Cix = cardvm.Cix, Discription = cardvm.Discription,
                              DueDate = cardvm.DueDate, BoardId = boardid, ListId = listid };
        }

        public static IEnumerable<Card> ViewModelsToModels(IEnumerable<CardViewModel> listvms, int boardid, int listid)
        {
            return listvms.Select(boardvm => ViewModelToModel(boardvm, boardid, listid)).ToList();
        }
        #endregion
    }
}