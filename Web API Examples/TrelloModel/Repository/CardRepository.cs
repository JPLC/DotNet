using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class CardRepository : IRepository<Card>
    {
        #region Variables and Properties
        private static readonly Lazy<CardRepository> CardRepo = new Lazy<CardRepository>(() => new CardRepository());

        public static CardRepository Instance { get { return CardRepo.Value; } }
        #endregion

        #region Constructor
        private CardRepository() { }
        #endregion

        //TODO aplicar e melhorar expection handling
        #region Methods
        public IEnumerable<Card> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.ToList();
            }
        }

        public Card GetSingle(int id)
        {
            return GetAll().FirstOrDefault(c => c.CardId == id);
        }

        public IEnumerable<Card> FindBy(Func<Card, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                card.Cix = db.List.Count(l => l.ListId == card.ListId) + 1;
                card.CreationDate = DateTime.Now;
                db.Card.Add(card);
                db.SaveChanges();
            }
        }

        public void Delete(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteCard(card.CardId, card.Cix, card.ListId);
            }
        }

        public void Edit(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditCard(card.CardId, card.Cix, card.Name, card.Discription, card.DueDate, card.ListId);
            }
        }
        #endregion
    }
}