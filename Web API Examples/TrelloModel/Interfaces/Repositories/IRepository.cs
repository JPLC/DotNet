using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetSingle(int id);

        T FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate);

        void Add(T t);

        void AddRange(IEnumerable<T> t);

        void Delete(T t);

        void DeleteRange(IEnumerable<T> t);

        void Edit(T t);

        void EditRange(IEnumerable<T> t);

        int Count();

        bool ValidId(int id);
    }
}
