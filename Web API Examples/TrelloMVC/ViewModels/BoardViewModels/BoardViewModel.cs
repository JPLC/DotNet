﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TrelloMVC.Validations.Attributes;
using TrelloMVC.Validations.Constants;
using TrelloMVC.Resources;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    public class BoardViewModel
    {
        [Required(ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameIsEmpty")]
        [RegularExpression(ModelRegexs.BoardNameRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameSpecialChars")]
        [StringLength(ModelSizeConstants.BoardNameSize, MinimumLength = 4, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameBiggerThanMaxValue")]
        [Remote("CheckBoardName", "Board", ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardNameAlreadyExists")]
        public string Name { get; set; }

        [RegularExpression(ModelRegexs.BoardDiscriptionRegex, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionSpecialChars")]
        [StringLength(ModelSizeConstants.BoardDiscriptionSize, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "BoardDiscriptionBiggerThanMaxValue")]
        [MaxWords(10, ErrorMessageResourceType = typeof(BoardResources), ErrorMessageResourceName = "DescriptionWordBiggerThanMaxValue")]
        public string Discription { get; set; }      
    }

}