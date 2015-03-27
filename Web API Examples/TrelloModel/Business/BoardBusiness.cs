using System.Collections.Generic;

namespace TrelloModel.Business
{
    public static class BoardBusiness
    {
        public static bool ValidateBoard(Board board, out List<KeyValuePair<string, string>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(board.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is mandatory"));
            }
            else if (board.Name.Length > 255)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is bigger than max value"));
            }
            if (string.IsNullOrEmpty(board.Discription))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Discription", "Discription is mandatory"));
            }
            else if (board.Discription.Length > 255)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Discription", "Discription is bigger than max value"));
            }
            return isValid;
        }
    }
}