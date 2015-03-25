using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class CardRepository :IRepository<Card>
    {
        public IEnumerable<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public Card GetSingle(int fooId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> FindBy(Expression<Func<Card, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Card entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Card entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Card entity)
        {
            throw new NotImplementedException();
        }
    }
}
