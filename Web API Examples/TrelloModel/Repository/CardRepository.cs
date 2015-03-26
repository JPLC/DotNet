using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class CardRepository : IRepository<Card>
    {

        private CardRepository()
        {
        }

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

        public void Add(Card entity)
        {
            using (var db = new TrelloModelDBContainer())
            {

            }
        }

        public void Delete(Card entity)
        {
            using (var db = new TrelloModelDBContainer())
            {

            }
        }

        public void Edit(Card entity)
        {
            using (var db = new TrelloModelDBContainer())
            {

            }
        }
    }
}
