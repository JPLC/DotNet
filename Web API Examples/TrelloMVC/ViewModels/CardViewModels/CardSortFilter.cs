﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloMVC.ViewModels.CardViewModels
{
    public class CardSortFilter
    {
        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }

        public string NameSortParm { get; set; }

        public string DiscriptionSortParm { get; set; }

        public string CixSortParm { get; set; }

        public string CDateSortParm { get; set; }

        public string DueDateSortParm { get; set; }
    }
}