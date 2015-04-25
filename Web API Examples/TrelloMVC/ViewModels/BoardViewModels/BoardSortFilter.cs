using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloMVC.ViewModels.BoardViewModels
{
    public class BoardSortFilter
    {
        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }

        public string NameSortParm { get; set; }

        public string DiscriptionSortParm { get; set; }
    }
}