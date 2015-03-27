using System.Collections.Generic;

namespace TrelloModel.Business
{
    public static class ListBusiness
    {
        public static bool ValidateList(List list, out List<KeyValuePair<string, string>> errorMsgDic)
        {
            var isValid = true;
            errorMsgDic = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(list.Name))
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is mandatory"));
            }
            else if (list.Name.Length > 255)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Name", "Name is bigger than max value"));
            }
            if (list.Lix < 0)
            {
                isValid = false;
                errorMsgDic.Add(new KeyValuePair<string, string>("Lix", "List index must be a positive number"));
            }
            return isValid;
        }
    }
}