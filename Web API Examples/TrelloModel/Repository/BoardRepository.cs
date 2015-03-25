using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class BoardRepository : IRepository<Board>
    {
        public IEnumerable<Board> GetAll()
        {
            throw new NotImplementedException();
        }

        public Board GetSingle(int fooId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Board> FindBy(Expression<Func<Board, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Board entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Board entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Board entity)
        {
            throw new NotImplementedException();
        }
    }
}
