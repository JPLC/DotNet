using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.SQL
{
    public class BoardRepositorySQL : IBoardRepository, IBoardRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<BoardRepositorySQL> BoardRepo = new Lazy<BoardRepositorySQL>(() => new BoardRepositorySQL());

        public static BoardRepositorySQL Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Constructor
        private BoardRepositorySQL() { }
        #endregion

        //TODO aplicar e melhorar expection handling
        // db.Database.Log = (msg) => { Console.WriteLine(msg ); };
        #region Sync Methods
        public IEnumerable<Board> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.ToList();
            }
        }

        public IEnumerable<Board> GetAllPaging(string searchString, int pagenumber, int pagesize)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return db.Board.Where(b => b.Name.Contains(searchString)).OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return db.Board.OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public Board GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.FirstOrDefault(b => b.BoardId == id);
            }
        }

        public Board FindBy(Expression<Func<Board, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.FirstOrDefault(predicate);
            }
        }

        public IEnumerable<Board> FindAllBy(Expression<Func<Board, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Where(predicate).ToList();
            }
        }

        public void Add(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Add(board);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.AddRange(boards);
                db.SaveChanges();
            }
        }

        public void Delete(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Where(b => b.BoardId == board.BoardId).Delete();
                db.SaveChanges();
            }
        }

        public void DeleteRange(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var aux = boards.Select(b => b.BoardId).ToList();
                db.Board.Where(p => aux.Contains(p.BoardId)).Delete();
                db.SaveChanges();
            }
        }

        public void Edit(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditRange(IEnumerable<Board> boards)
        {
            foreach (var board in boards)
            {
                Edit(board);
            }
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Count();
            }
        }

        public bool HasRepeatedBoardName(int boardid, string boardname)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Any(b => b.Name == boardname && b.BoardId != boardid);
            }
        }

        public bool ValidId(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Board.Any(b => b.BoardId == id);
            }
        }
        #endregion
        
        #region Async Methods
        public async Task<IEnumerable<Board>> GetAllAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.ToListAsync();
            }
        }

        public async Task<IEnumerable<Board>> GetAllPagingAsync(string searchString, int pagenumber, int pagesize)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return await db.Board.Where(b => b.Name.Contains(searchString)).OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
                }
                return await db.Board.OrderBy(b => b.Name).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
            }
        }

        public async Task<Board> GetSingleAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.FirstOrDefaultAsync(b => b.BoardId == id);
            }
        }

        public async Task<Board> FindByAsync(Expression<Func<Board, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.FirstOrDefaultAsync(predicate);
            }
        }

        public async Task<IEnumerable<Board>> FindAllByAsync(Expression<Func<Board, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.Where(predicate).ToListAsync();
            }
        }

        public async Task<Board> AddAsync(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Add(board);
                await db.SaveChangesAsync();
                return board;
            }
        }

        public async Task AddRangeAsync(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.AddRange(boards);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Board.Where(b => b.BoardId == board.BoardId).Delete();
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Board> boards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var aux = boards.Select(b => b.BoardId).ToList();
                db.Board.Where(p => aux.Contains(p.BoardId)).Delete();
                await db.SaveChangesAsync();
            }
        }

        public async Task EditAsync(Board board)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.Entry(board).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task EditRangeAsync(IEnumerable<Board> boards)
        {
            foreach (var board in boards)
            {
                await EditAsync(board);
            }
        }

        public async Task<int> CountAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.CountAsync();
            }
        }

        public async Task<bool> HasRepeatedBoardNameAsync(int boardid, string boardname)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.AnyAsync(b => b.Name == boardname && b.BoardId != boardid);
            }
        }

        public async Task<bool> ValidIdAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Board.AnyAsync(b => b.BoardId == id);
            }
        }
        #endregion
    }
}