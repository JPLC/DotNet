//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrelloModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Board
    {
        public Board()
        {
            this.Lists = new HashSet<List>();
            this.Cards = new HashSet<Card>();
        }
    
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
    
        public virtual ICollection<List> Lists { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
