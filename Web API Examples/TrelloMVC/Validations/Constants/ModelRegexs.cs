using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloMVC.Validations.Constants
{
    public class ModelRegexs
    {
        #region Regex Constants
        public const string BoardNameRegex = @"^[A-Za-z 0-9]+$";
        public const string BoardDiscriptionRegex = @"^[A-Za-z 0-9]+$";
        public const string ListNameRegex = @"^[A-Za-z 0-9]+$";
        public const string CardNameRegex = @"^[A-Za-z 0-9]+$";
        public const string CardDiscriptionRegex = @"^[A-Za-z 0-9]+$";
        #endregion
    }
}