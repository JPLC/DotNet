using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class ListRespository : IRepository<List>
    {
        public IEnumerable<List> GetAll()
        {
            throw new NotImplementedException();
        }

        public List GetSingle(int fooId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List> FindBy(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(List entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(List entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(List entity)
        {
            throw new NotImplementedException();
        }
    }
}
