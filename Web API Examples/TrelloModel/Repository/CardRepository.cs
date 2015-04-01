using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using TrelloModel.Business;
using TrelloModel.Business.Enumerators;
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
                card.Cix = db.Card.Count(ca => ca.ListId == card.ListId) + 1;
                card.CreationDate = DateTime.Now;
                db.Card.Add(card);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var i = 1;
                foreach (var c in cards)
                {
                    c.Cix = db.Card.Count(ca => ca.ListId == c.ListId) + i;
                    c.CreationDate = DateTime.Now;
                    db.Card.Add(c);
                    i++;
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
                List<KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
                if (CardBusiness.ValidateCard(card, out errorMsgDic))
                {
                    db.EditCard(card.CardId, card.Cix, card.Name, card.Discription, card.DueDate, card.ListId);
                }
                else
                {
                    throw new DbEntityValidationException("Card validation error");
                }
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

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.Card.Count();
            }
        }
        #endregion
    }
}