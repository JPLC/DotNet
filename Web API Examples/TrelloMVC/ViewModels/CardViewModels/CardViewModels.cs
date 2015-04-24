using System;

namespace TrelloMVC.ViewModels.CardViewModels
{
    /*TODO por os atributos de validacao*/
    public class CardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cix { get; set; }

        public string Discription { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DueDate { get; set; }

        public string ListName { get; set; }
    }
}