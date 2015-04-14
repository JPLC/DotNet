using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrelloMVC.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Remote("CheckUserName", "Manage")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Email")]
        public string EmailConfirm { get; set; }
    }
}