using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TrelloMVC.Validations.Constants;
using Resx=TrelloMVC.Validations.Resources;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    public class BoardViewModel
    {
        [Required(ErrorMessage = Resx.BoardResources.BoardNameIsEmpty)]
        [RegularExpression(ModelRegexs.BoardNameRegex, ErrorMessage = Resx.BoardResources.BoardNameSpecialChars)]
        [StringLength(ModelSizeConstants.BoardNameSize, MinimumLength = 4, ErrorMessage = ErrorMessage = Resx.BoardResources.BoardNameBiggerThanMaxValue)]
        [Remote("CheckBoardName", "Board", ErrorMessage = Resx.BoardResources.BoardNameAlreadyExists)]
        public string Name { get; set; }

        [RegularExpression(ModelRegexs.BoardDiscriptionRegex, ErrorMessage = Resx.BoardResources.BoardDiscriptionSpecialChars)]
        [StringLength(ModelSizeConstants.BoardDiscriptionSize, ErrorMessage = Resx.BoardResources.BoardDiscriptionBiggerThanMaxValue)]
        public string Discription { get; set; }      
    }

}