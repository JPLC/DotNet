﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class CardRepository : IRepository<Card>
    {
        private static readonly Lazy<CardRepository> CardRepo = new Lazy<CardRepository>(() => new CardRepository());

        public static CardRepository Instance { get { return CardRepo.Value; } }

        private CardRepository() { }

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
                db.Card.Add(card);
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
                db.EditCard(card.CardId, card.Cix, card.Name, card.Discription, card.CreationDate, card.DueDate, card.ListId);
            }
        }
    }
}