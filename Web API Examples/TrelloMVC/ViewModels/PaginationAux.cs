using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloMVC.ViewModels
{
    public class PaginationAux
    {
        public int ElementsCount { get; set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}