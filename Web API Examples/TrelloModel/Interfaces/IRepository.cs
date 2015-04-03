using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T GetSingle(int id);

        Task<T> GetSingleAsync(int id);

        T FindBy(Expression<Func<T, bool>> predicate);

        Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindAllByAsync(Expression<Func<T, bool>> predicate);

        void Add(T t);

        Task AddAsync(T t);

        void AddRange(IEnumerable<T> t);

        Task AddRangeAsync(IEnumerable<T> t);

        void Delete(T t);

        Task DeleteAsync(T t);

        void DeleteRange(IEnumerable<T> t);

        Task DeleteRangeAsync(IEnumerable<T> t);

        void Edit(T t);

        Task EditAsync(T t);

        void EditRange(IEnumerable<T> t);

        Task EditRangeAsync(IEnumerable<T> t);

        int Count();

        Task<int> CountAsync();
    }
}
