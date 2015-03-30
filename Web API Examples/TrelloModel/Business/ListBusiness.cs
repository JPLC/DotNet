using System.Collections.Generic;
using TrelloModel.Business.Constants;
using TrelloModel.Business.Enumerators;

namespace TrelloModel.Business
{
    public static class ListBusiness
    {
        public static bool ValidateList(List list, out List<KeyValuePair<ListValidationCodes, KeyValuePair<string, string>>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<ListValidationCodes, KeyValuePair<string,string>>>();
            if (list.Name == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<ListValidationCodes, KeyValuePair<string,string>>(ListValidationCodes.ListNameIsNull, new KeyValuePair<string, string>("Name", "Name is mandatory")));
            }
            else if (list.Name == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<ListValidationCodes, KeyValuePair<string,string>>(ListValidationCodes.ListNameIsEmpty, new KeyValuePair<string, string>("Name", "Name is mandatory")));
            }
            else if (list.Name.Length > TrelloSizeConstants.ListNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<ListValidationCodes, KeyValuePair<string,string>>(ListValidationCodes.ListNameBiggerThanMaxValue,new KeyValuePair<string, string>("Name", "Name is bigger than max value")));
            }
            else if (!TrelloRegularExpressions.IsValidBoardName(list.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<ListValidationCodes, KeyValuePair<string, string>>(ListValidationCodes.ListNameSpecialChars, new KeyValuePair<string, string>("Name", "Name has special characters")));
            }
            if (list.Lix < 0)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<ListValidationCodes, KeyValuePair<string, string>>(ListValidationCodes.ListIndexNegative ,new KeyValuePair<string, string>("Lix", "List index must be a positive integer")));
            }
            return isValid;
        }
    }
}