using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.SQL
{
    public class ListRepositorySQL : IListRepository, IListRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<ListRepositorySQL> ListRepo = new Lazy<ListRepositorySQL>(() => new ListRepositorySQL());

        public static ListRepositorySQL Instance { get { return ListRepo.Value; } }
        #endregion

        #region Constructor
        private ListRepositorySQL() { }
        #endregion
        
        //TODO aplicar e melhorar expection handling
        #region Sync Methods
        //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
        public IEnumerable<List> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.ToList();
            }
        }

        public IEnumerable<List> GetListsOfBoardPaging(Expression<Func<List, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize, int boardid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                             db.List.Where(b => b.BoardId == boardid && b.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                           : db.List.Where(b => b.BoardId == boardid && b.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                         db.List.Where(b => b.BoardId == boardid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                       : db.List.Where(b => b.BoardId == boardid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public IEnumerable<List> GetListsOfBoardPaging(Expression<Func<List, int>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize, int boardid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                        db.List.Where(b => b.BoardId == boardid && b.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                        : db.List.Where(b => b.BoardId == boardid && b.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                    db.List.Where(b => b.BoardId == boardid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                    : db.List.Where(b => b.BoardId == boardid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }
        
        public List GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.FirstOrDefault(l => l.ListId == id);
            }
        }

        public List FindBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<List> FindAllBy(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(predicate).ToList();
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
        public void Delete(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteList(list.ListId, list.Lix, list.BoardId);
            }
        }

        public void DeleteRange(IEnumerable<List> lists)
        {
            foreach (var l in lists)
            {
                Delete(l);
            }
        }

        public void Edit(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditList(list.ListId, list.Lix, list.BoardId, list.Name);
            }
        }

        public IEnumerable<List> GetListsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(l => l.BoardId == boardId).ToList();
            }
        }

        public void EditRange(IEnumerable<List> lists)
        {
            foreach (var list in lists)
            {
                Edit(list);
            }
        }

        public string GetListBoardName(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Where(l => l.BoardId == boardId).Select(b => b.Name).FirstOrDefault();
            }
        }

        public int CountListsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Count(l => l.BoardId==boardId);
            }
        }

        public int CountConditionalListsOfBoard(Expression<Func<List, bool>> predicate, int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(predicate).Count(l => l.BoardId == boardId);
            }
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Count();
            }
        }

        public bool ValidId(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Any(l => l.ListId == id);
            }
        }
        #endregion
        
        #region Async Methods
        public async Task<IEnumerable<List>> GetAllAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.ToListAsync();
            }
        }

        public async Task<IEnumerable<List>> GetListsOfBoardAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(l => l.BoardId == boardId).ToListAsync();
            }
        }

        public async Task<IEnumerable<List>> GetListsOfBoardPagingAsync(Expression<Func<List, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize, int boardid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                        await db.List.Where(b => b.BoardId==boardid && b.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                       : await db.List.Where(b => b.BoardId == boardid && b.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
                }
                return direction == SortDirection.Ascending ?
                    await db.List.Where(b => b.BoardId == boardid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                  : await db.List.Where(b => b.BoardId == boardid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
            }
        }

        public async Task<List> GetSingleAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.FirstOrDefaultAsync(l => l.ListId == id);
            }
        }

        public async Task<List> FindByAsync(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(predicate).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<List>> FindAllByAsync(Expression<Func<List, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(predicate).ToListAsync();
            }
        }

        Task<List> IRepositoryAsync<List>.AddAsync(List t)
        {
            throw new NotImplementedException();
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

        public Task DeleteAsync(List t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(List t)
        {
            throw new NotImplementedException();
        }

        public Task EditRangeAsync(IEnumerable<List> t)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetListBoardNameAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.Where(l => l.BoardId == boardId).Select(b => b.Name).FirstOrDefaultAsync();
            }
        }

        public async Task<int> CountListsOfBoardAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.CountAsync(l => l.BoardId == boardId);
            }
        }

        public async Task<int> CountConditionalListsOfBoardAsync(Expression<Func<List, bool>> predicate, int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(predicate).CountAsync(l => l.BoardId == boardId);
            }
        }

        public async Task<int> CountAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.CountAsync();
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