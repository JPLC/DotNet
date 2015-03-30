using System.Collections.Generic;
using TrelloModel.Business.Constants;
using TrelloModel.Business.Enumerators;

namespace TrelloModel.Business
{
    public static class CardBusiness
    {
        public static bool ValidateCard(Card card, out List<KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>>();
            if (card.Name == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameIsNull, new KeyValuePair<string, string>("Name", "Name is mandatory")));
            }
            else if (card.Name == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameIsEmpty, new KeyValuePair<string, string>("Name", "Name is mandatory")));
            }
            else if (card.Name.Length > TrelloSizeConstants.CardNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameBiggerThanMaxValue, new KeyValuePair<string, string>("Name", "Name is bigger than max value")));
            }
            else if (!TrelloRegularExpressions.IsValidCardName(card.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameSpecialChars, new KeyValuePair<string, string>("Name", "Name has special characters")));
            }

            if (card.Discription == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionIsEmpty, new KeyValuePair<string, string>("Discription", "Discription is mandatory")));
            }
            else if (card.Discription == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscpriptionIsNull, new KeyValuePair<string, string>("Discription", "Discription is mandatory")));
            }
            else if (card.Discription.Length > TrelloSizeConstants.CardNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionBiggerThanMaxValue, new KeyValuePair<string, string>("Discription", "Discription is bigger than max value")));
            }
            else if (!TrelloRegularExpressions.IsValidCardDiscription(card.Discription))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionSpecialChars, new KeyValuePair<string, string>("Discription", "Discription has special characters")));
            }
            if (card.Cix < 0)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardIndexNegative, new KeyValuePair<string, string>("Cix", "Card index must be a positive number")));
            }

            if (card.CreationDate > card.DueDate)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardCreationDateSuperiorToDueDate, new KeyValuePair<string, string>("DueDate", "DueDate must be after CreationDate")));
            }
            return isValid;
        }
    }
}