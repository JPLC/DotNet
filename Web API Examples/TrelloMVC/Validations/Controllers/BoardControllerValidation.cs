using System.Collections.Generic;
using System.Web.Mvc;
using TrelloModel.Business;
using TrelloModel.Business.Enumerators;
using TrelloMVC.ViewModels.BoardViewModels;
using TrelloMVC.ViewModels.Converters;

namespace TrelloMVC.Validations.Controllers
{
    public static class BoardControllerValidation
    {
        public static void BoardValidator(ModelStateDictionary ms, BoardViewModel boardvm)
        {
            List<KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
            BoardBusiness.ValidateBoard(VMConverters.ViewModelToModel(boardvm), null, out errorMsgDic);
            foreach (var error in errorMsgDic)
            {
                ms.AddModelError(error.Value.Key,error.Value.Value);
            }
        }
    }
}