using System.Collections.Generic;
using TrelloModel.Business.Constants;
using TrelloModel.Business.Enumerators;
using Resx = TrelloModel.Resources;

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
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameIsNull, new KeyValuePair<string, string>("Name", Resx.CardResources.CardNameIsNull)));
            }
            else if (card.Name == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameIsEmpty, new KeyValuePair<string, string>("Name", Resx.CardResources.CardNameIsEmpty)));
            }
            else if (card.Name.Length > TrelloSizeConstants.CardNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameBiggerThanMaxValue, new KeyValuePair<string, string>("Name", Resx.CardResources.CardNameBiggerThanMaxValue)));
            }
            else if (!TrelloRegularExpressions.IsValidCardName(card.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardNameSpecialChars, new KeyValuePair<string, string>("Name", Resx.CardResources.CardNameSpecialChars)));
            }

            if (card.Discription == string.Empty)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionIsEmpty, new KeyValuePair<string, string>("Discription", Resx.CardResources.CardDiscriptionIsEmpty)));
            }
            else if (card.Discription == null)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscpriptionIsNull, new KeyValuePair<string, string>("Discription", Resx.CardResources.CardDiscpriptionIsNull)));
            }
            else if (card.Discription.Length > TrelloSizeConstants.CardNameSize)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionBiggerThanMaxValue, new KeyValuePair<string, string>("Discription", Resx.CardResources.CardDiscriptionBiggerThanMaxValue)));
            }
            else if (!TrelloRegularExpressions.IsValidCardDiscription(card.Discription))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardDiscriptionSpecialChars, new KeyValuePair<string, string>("Discription", Resx.CardResources.CardDiscriptionSpecialChars)));
            }
            if (card.Cix < 0)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardIndexNegative, new KeyValuePair<string, string>("Cix", Resx.CardResources.CardIndexNegative)));
            }

            if (card.CreationDate > card.DueDate)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>(CardValidationCodes.CardCreationDateSuperiorToDueDate, new KeyValuePair<string, string>("DueDate", Resx.CardResources.CardCreationDateSuperiorToDueDate)));
            }
            return isValid;
        }
    }
}