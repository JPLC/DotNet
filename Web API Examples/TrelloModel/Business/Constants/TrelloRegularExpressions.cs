using System.Text.RegularExpressions;

namespace TrelloModel.Business.Constants
{
    public static class TrelloRegularExpressions
    {
        #region Regex Constants
        public const string BoardNameRegex = @"^[A-Za-z 0-9]+$";
        public const string BoardDiscriptionRegex = @"^[A-Za-z 0-9]+$";
        public const string ListNameRegex = @"^[A-Za-z 0-9]+$";
        public const string CardNameRegex = @"^[A-Za-z 0-9]+$";
        public const string CardDiscriptionRegex = @"^[A-Za-z 0-9]+$";
        #endregion

        #region Methods
        public static bool IsValidBoardName(string boardName)
        {
            return Regex.IsMatch(boardName, BoardNameRegex);
        }

        public static bool IsValidBoardDiscription(string boardDiscription)
        {
            return Regex.IsMatch(boardDiscription, BoardDiscriptionRegex);
        }

        public static bool IsValidListName(string listName)
        {
            return Regex.IsMatch(listName, ListNameRegex);
        }

        public static bool IsValidCardName(string cardName)
        {
            return Regex.IsMatch(cardName, CardNameRegex);
        }

        public static bool IsValidCardDiscription(string cardDiscription)
        {
            return Regex.IsMatch(cardDiscription, CardDiscriptionRegex);
        }
        #endregion
    }
}