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
    
    public partial class Card
    {
        public int CardId { get; set; }
        public int Cix { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public int BoardId { get; set; }
        public int ListId { get; set; }
    
        public virtual Board Board { get; set; }
        public virtual List List { get; set; }
    }
}
