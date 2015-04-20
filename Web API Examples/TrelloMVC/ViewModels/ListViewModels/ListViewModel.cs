using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TrelloModel.Business.Constants;
using TrelloModel.Resources;

namespace TrelloMVC.ViewModels.ListViewModels
{
    [Bind(Include = "Name, Lix")]
    public class ListViewModel
    {
        [ScaffoldColumn(false)]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Display(Name = "List Name", Order = 15001)]
        [Required(ErrorMessageResourceType = typeof(ListResources), ErrorMessageResourceName = "ListNameIsEmpty")]
        [RegularExpression(TrelloRegularExpressions.BoardNameRegex, ErrorMessageResourceType = typeof(ListResources), ErrorMessageResourceName = "ListNameSpecialChars")]
        [StringLength(TrelloSizeConstants.BoardNameSize, MinimumLength = 4, ErrorMessageResourceType = typeof(ListResources), ErrorMessageResourceName = "ListNameBiggerThanMaxValue")]
        public string Name { get; set; }

        [Display(Name = "Index", Order = 15000)]
        [Required(ErrorMessageResourceType = typeof(ListResources), ErrorMessageResourceName = "ListIndexMandatory")]
        public int Lix { get; set; }

        public string BoardName { get; set; }
    }
}