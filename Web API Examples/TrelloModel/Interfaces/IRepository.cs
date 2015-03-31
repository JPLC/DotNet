using System;
using System.Collections.Generic;

namespace TrelloModel.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetSingle(int id);

        IEnumerable<T> FindBy(Func<T,bool> predicate);

        void Add(T t);

        void AddRange(IEnumerable<T> t);

        void Delete(T t);

        void Edit(T t);
    }
}
