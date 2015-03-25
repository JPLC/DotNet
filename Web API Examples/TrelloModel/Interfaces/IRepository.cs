using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrelloModel.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetSingle(int id);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T t);

        void Delete(T t);

        void Edit(T t);
    }
}
