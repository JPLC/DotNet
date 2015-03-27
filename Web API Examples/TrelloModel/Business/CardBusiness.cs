using System;
using System.Collections.Generic;


namespace TrelloModel.Business
{
    public static class CardBusiness
    {
        public static bool ValidateCard(Card card, out List<KeyValuePair<string, string>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(card.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is mandatory"));
            }
            else if (card.Name.Length > 255)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is bigger than max value"));
            }
            if (card.Cix < 0)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Cix", "Card index must be a positive number"));
            }
            if (string.IsNullOrEmpty(card.Discription))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Discription", "Discription is mandatory"));
            }
            else if (card.Discription.Length > 255)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Discription", "Discription is bigger than max value"));
            }
            if (card.CreationDate > card.DueDate)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("DueDate", "DueDate must be after CreationDate"));
            }
            return isValid;
        }
    }
}
