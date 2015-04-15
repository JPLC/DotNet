using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TrelloModel.Resources;
using TrelloMVC.Validations.Attributes;
using TrelloMVC.Validations.Constants;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    [Bind(Include = "Name, Discription")]
    public class BoardsViewModel
    {
        [ScaffoldColumn(false)]
        [ReadOnly(true)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]

        [Display(Name = "Board Name", Order = 15000)]
        [Required(ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameIsEmpty")]
        [RegularExpression(ModelRegexs.BoardNameRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameSpecialChars")]
        [StringLength(ModelSizeConstants.BoardNameSize, MinimumLength = 4, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameBiggerThanMaxValue")]
        [Remote("CheckBoardName", "Board", ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameAlreadyExists")]
        public string Name { get; set; }

        [Display(Name = "Board Discription", Order = 15001)]
        [RegularExpression(ModelRegexs.BoardDiscriptionRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionSpecialChars")]
        [StringLength(ModelSizeConstants.BoardDiscriptionSize, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionBiggerThanMaxValue")]
        [MaxWords(10)]
        public string Discription { get; set; }
    }
}