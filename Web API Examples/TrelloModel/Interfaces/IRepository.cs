using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrelloModel.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetSingle(int fooId);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);
    }
}
