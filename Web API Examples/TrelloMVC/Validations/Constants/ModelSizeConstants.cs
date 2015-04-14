using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloMVC.Validations.Constants
{
    public class ModelSizeConstants
    {
        #region Board Constants
        public const int BoardNameSize = 50;
        public const int BoardDiscriptionSize = 255;
        #endregion

        #region List Constants
        public const int ListNameSize = 50;
        #endregion

        #region Card Constants
        public const int CardNameSize = 50;
        public const int CardDiscriptionSize = 255;
        #endregion
    }
}