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

        public async Task<IEnumerable<List>> GetAllAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.ToListAsync();
            }
        }

        public IEnumerable<List> GetAllPaging(string searchString, int pagenumber, int pagesize)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return db.List.Where(b => b.Name.Contains(searchString)).OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return db.List.OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public List GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.FirstOrDefault(l => l.ListId == id);
            }
        }

        public async Task<List> GetSingleAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.FirstOrDefaultAsync(l => l.ListId == id);
            }
        }

        public List FindBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Where(predicate).FirstOrDefault();
            }
        }

        public async Task<List> FindByAsync(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return await db.List.Where(predicate).FirstOrDefaultAsync();
            }
        }

        public IEnumerable<List> FindAllBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(predicate).ToList();
            }
        }

        public async Task<IEnumerable<List>> FindAllByAsync(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(predicate).ToListAsync();
            }
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

        public async Task AddAsync(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                list.Lix = db.List.Count(li => li.BoardId == list.BoardId) + 1;
                db.List.Add(list);
                await db.SaveChangesAsync();
            }
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

        public async Task AddRangeAsync(IEnumerable<List> lists)
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
                await db.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(l => l.BoardId == boardId).ToListAsync();
            }
        }

        public string GetListBoardName(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Where(l => l.BoardId == boardId).Select(b=>b.Name).FirstOrDefault();
            }
        }

        public async Task<string> GetListBoardNameAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.Where(l => l.BoardId == boardId).Select(b => b.Name).FirstOrDefaultAsync();
            }
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
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