using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    public class BoardViewModel
    {
        [Required]
        [StringLength(255, MinimumLength=3)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Discription { get; set; }      
    }

}