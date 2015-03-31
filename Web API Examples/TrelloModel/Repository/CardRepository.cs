using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class CardRepository : ICardRepository
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
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.FirstOrDefault(c => c.CardId == id);
            }
        }

        public Card FindBy(Func<Card, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<Card> FindAllBy(Func<Card, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).ToList();
            }
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

        public void AddRange(IEnumerable<Card> cards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                foreach (var c in cards)
                {
                    c.Cix = db.List.Count(l => l.ListId == c.ListId) + 1;
                    c.CreationDate = DateTime.Now;
                    db.Card.Add(c);
                }
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

        public void DeleteRange(IEnumerable<Card> cards)
        {
            foreach (var c in cards)
            {
                Delete(c);
            }
        }

        public void Edit(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditCard(card.CardId, card.Cix, card.Name, card.Discription, card.DueDate, card.ListId);
            }
        }

        public void EditRange(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                Edit(card);
            }
        }

        public IEnumerable<Card> GetCardsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(c => c.BoardId == boardId).ToList();
            }
        }

        public IEnumerable<Card> GetCardsOfList(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(c => c.ListId == listId).ToList();
            }
        }
        #endregion
    }
}