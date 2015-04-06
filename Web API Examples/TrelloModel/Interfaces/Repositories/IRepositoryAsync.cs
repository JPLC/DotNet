using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrelloModel.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetSingleAsync(int id);

        Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindAllByAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T t);

        Task AddRangeAsync(IEnumerable<T> t);

        Task DeleteAsync(T t);

        Task DeleteRangeAsync(IEnumerable<T> t);

        Task EditAsync(T t);

        Task EditRangeAsync(IEnumerable<T> t);

        Task<int> CountAsync();
    }
}
