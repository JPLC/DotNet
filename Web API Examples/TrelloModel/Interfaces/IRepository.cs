using System;
using System.Collections.Generic;

namespace TrelloModel.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetSingle(int id);

        T FindBy(Func<T, bool> predicate);

        IEnumerable<T> FindAllBy(Func<T,bool> predicate);

        void Add(T t);

        void AddRange(IEnumerable<T> t);

        void Delete(T t);

        void DeleteRange(IEnumerable<T> t);

        void Edit(T t);
    }
}
