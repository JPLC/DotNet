using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TrelloModel.Resources;
using TrelloModel.Business.Constants;
using TrelloMVC.Validations.Attributes;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    [Bind(Include = "Name, Discription")]
    public class BoardViewModel
    {
        [ScaffoldColumn(false)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Display(Name = "Board Name", Order = 15000)]
        [Required(ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameIsEmpty")]
        [RegularExpression(TrelloRegularExpressions.BoardNameRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameSpecialChars")]
        [StringLength(TrelloSizeConstants.BoardNameSize, MinimumLength = 4, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameBiggerThanMaxValue")]
        //[Remote("CheckBoardName", "Board", ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameAlreadyExists")]
        public string Name { get; set; }

        [Display(Name = "Board Discription", Order = 15001)]
        [RegularExpression(TrelloRegularExpressions.BoardDiscriptionRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionSpecialChars")]
        [StringLength(TrelloSizeConstants.BoardDiscriptionSize, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionBiggerThanMaxValue")]
        [MaxWords(10)]
        public string Discription { get; set; }
    }
}