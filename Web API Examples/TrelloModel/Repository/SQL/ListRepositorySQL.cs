using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.SQL
{
    public class ListRepositorySQL : IListRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<ListRepositorySQL> ListRepo = new Lazy<ListRepositorySQL>(() => new ListRepositorySQL());

        public static ListRepositorySQL Instance { get { return ListRepo.Value; } }
        #endregion

        #region Constructor
        private ListRepositorySQL() { }
        #endregion

        //TODO aplicar e melhorar expection handling
        #region Methods
        public IEnumerable<List> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.ToList();
            }
        }

        public Task<IEnumerable<List>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public List GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.FirstOrDefault(l => l.ListId == id);
            }
        }

        public Task<List> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List FindBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Where(predicate).FirstOrDefault();
            }
        }

        public Task<List> FindByAsync(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List> FindAllBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(predicate).ToList();
            }
        }

        public Task<IEnumerable<List>> FindAllByAsync(Expression<Func<List, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                list.Lix = db.List.Count(li => li.BoardId == list.BoardId) + 1;
                db.List.Add(list);
                db.SaveChanges();
            }
        }

        public Task AddAsync(List t)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<List> lists)
        {
            using (var db = new TrelloModelDBContainer())
            {
                int i = 1;
                foreach (var l in lists)
                {
                    l.Lix = db.List.Count(li => li.BoardId == l.BoardId) + i;
                    db.List.Add(l);
                    i++;
                }
                db.SaveChanges();
            }
        }

        public Task AddRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public void Delete(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteList(list.ListId, list.Lix, list.BoardId);
            }
        }

        public Task DeleteAsync(List t)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<List> lists)
        {
            foreach (var l in lists)
            {
                Delete(l);
            }
        }

        public Task DeleteRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public void Edit(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditList(list.ListId, list.Lix, list.BoardId, list.Name);
            }
        }

        public Task EditAsync(List t)
        {
            throw new NotImplementedException();
        }

        public void EditRange(IEnumerable<List> lists)
        {
            foreach (var list in lists)
            {
                Edit(list);
            }
        }

        public Task EditRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List> GetListsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(l => l.BoardId == boardId).ToList();
            }
        }

        public Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Count();
            }
        }

        public async Task<int> CountAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.CountAsync();
            }
        }

        public bool ValidId(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Any(l => l.ListId == id);
            }
        }

        public async Task<bool> ValidIdAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.AnyAsync(l => l.ListId == id);
            }
        }
        #endregion
    }
}