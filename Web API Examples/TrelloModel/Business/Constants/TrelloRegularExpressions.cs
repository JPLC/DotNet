using System.Text.RegularExpressions;

namespace TrelloModel.Business.Constants
{
    public static class TrelloRegularExpressions
    {
        #region Regex Constants
        public const string BoardNameRegex = "";
        public const string BoardDiscriptionRegex = "";
        public const string ListNameRegex = "";
        public const string CardNameRegex = "";
        public const string CardDiscriptionRegex = "";
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