using System.Collections.Generic;
using System.Linq;
using TrelloModel.Business.Constants;
using TrelloModel.Business.Enumerators;
using Resx = TrelloModel.Resources;

namespace TrelloModel.Business
{
    public static class BoardBusiness
    {
        public static bool ValidateBoard(Board board, List<Board> boardNames, out List<KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<BoardValidationCodes, KeyValuePair<string,string>>>();
            if (board.Name == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardNameIsNull, new KeyValuePair<string, string>("Name", Resx.BoardResources.BoardDiscpriptionIsNull)));
            }
            else if (board.Name == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardNameIsEmpty, new KeyValuePair<string, string>("Name", Resx.BoardResources.BoardDiscriptionIsEmpty)));
            }
            else if (board.Name.Length > TrelloSizeConstants.BoardNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string,string>>(BoardValidationCodes.BoardNameBiggerThanMaxValue, new KeyValuePair<string, string>("Name", Resx.BoardResources.BoardNameBiggerThanMaxValue)));
            }
            else if (!TrelloRegularExpressions.IsValidBoardName(board.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardNameSpecialChars, new KeyValuePair<string, string>("Name", Resx.BoardResources.BoardNameSpecialChars)));
            }
            else if (boardNames!=null && boardNames.Any(b => b.BoardId != board.BoardId && b.Name == board.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardNameAlreadyExists, new KeyValuePair<string, string>("Name", Resx.BoardResources.BoardNameAlreadyExists)));
            }

            if (board.Discription == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardDiscriptionIsEmpty, new KeyValuePair<string, string>("Discription", Resx.BoardResources.BoardDiscriptionIsEmpty)));
            }
            else if (board.Discription == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardDiscpriptionIsNull, new KeyValuePair<string, string>("Discription", Resx.BoardResources.BoardDiscpriptionIsNull)));
            }
            else if (board.Discription.Length > TrelloSizeConstants.BoardDiscriptionSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardDiscriptionBiggerThanMaxValue, new KeyValuePair<string, string>("Discription", Resx.BoardResources.BoardDiscriptionBiggerThanMaxValue)));
            }
            else if (!TrelloRegularExpressions.IsValidBoardDiscription(board.Discription))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>(BoardValidationCodes.BoardDiscriptionSpecialChars, new KeyValuePair<string, string>("Discription", Resx.BoardResources.BoardDiscriptionSpecialChars)));
            }
            return isValid;
        }
    }
}