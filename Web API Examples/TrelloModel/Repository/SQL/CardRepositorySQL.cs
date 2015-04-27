using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloModel.Business;
using TrelloModel.Business.Enumerators;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.SQL
{
    public class CardRepositorySQL : ICardRepository, ICardRepositoryAsync
    {
        #region Variables and Properties
        private static readonly Lazy<CardRepositorySQL> CardRepo = new Lazy<CardRepositorySQL>(() => new CardRepositorySQL());

        public static CardRepositorySQL Instance { get { return CardRepo.Value; } }
        #endregion

        #region Constructor
        private CardRepositorySQL() { }
        #endregion

        //TODO aplicar e melhorar expection handling       
        #region Sync Methods
        public IEnumerable<Card> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.ToList();
            }
        }

        public IEnumerable<Card> GetAllPaging(Expression<Func<Card, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ? db.Card.Where(b => b.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList() : db.Card.Where(b => b.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ? db.Card.OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList() : db.Card.OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public Card GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.FirstOrDefault(c => c.CardId == id);
            }
        }

        public Card FindBy(Expression<Func<Card, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<Card> FindAllBy(Expression<Func<Card, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).ToList();
            }
        }

        public void Add(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                card.Cix = db.Card.Count(ca => ca.ListId == card.ListId) + 1;
                card.CreationDate = DateTime.Now;
                db.Card.Add(card);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var i = 1;
                foreach (var c in cards)
                {
                    c.Cix = db.Card.Count(ca => ca.ListId == c.ListId) + i;
                    c.CreationDate = DateTime.Now;
                    db.Card.Add(c);
                    i++;
                }
                db.SaveChanges();
            }
        }

        public void Delete(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteCard(card.CardId, card.Cix, card.ListId);
            }
        }

        public void DeleteRange(IEnumerable<Card> cards)
        {
            foreach (var c in cards)
            {
                Delete(c);
            }
        }

        public void Edit(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                List<KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
                if (CardBusiness.ValidateCard(card, out errorMsgDic))
                {
                    db.EditCard(card.CardId, card.Cix, card.Name, card.Discription, card.DueDate, card.ListId);
                }
                else
                {
                    throw new DbEntityValidationException("Card validation error");
                }
            }
        }

        public void EditRange(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                Edit(card);
            }
        }

        public IEnumerable<Card> GetCardsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(c => c.BoardId == boardId).ToList();
            }
        }

        public IEnumerable<Card> GetCardsOfBoardPaging<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction, string searchString, int pagenumber,
            int pagesize, int boardid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                             db.Card.Where(c => c.BoardId == boardid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                           : db.Card.Where(c => c.BoardId == boardid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                         db.Card.Where(c => c.BoardId == boardid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                       : db.Card.Where(c => c.BoardId == boardid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public IEnumerable<Card> GetCardsOfList(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(c => c.ListId == listId).ToList();
            }
        }

        public IEnumerable<Card> GetCardsOfListPaging<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction, string searchString, int pagenumber,
            int pagesize, int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                             db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                           : db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                         db.Card.Where(c => c.ListId == listid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                       : db.Card.Where(c => c.ListId == listid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }
        /*
        public IEnumerable<Card> GetCardsOfListPaging(Expression<Func<Card, int>> sorter, SortDirection direction, string searchString, int pagenumber,
        int pagesize, int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                             db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                           : db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                         db.Card.Where(c => c.ListId == listid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                       : db.Card.Where(c => c.ListId == listid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }

        public IEnumerable<Card> GetCardsOfListPaging(Expression<Func<Card, DateTime>> sorter, SortDirection direction, string searchString, int pagenumber,
        int pagesize, int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                             db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                           : db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
                }
                return direction == SortDirection.Ascending ?
                         db.Card.Where(c => c.ListId == listid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList()
                       : db.Card.Where(c => c.ListId == listid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            }
        }
        */
        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Count();
            }
        }

        public bool ValidId(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Any(c => c.CardId == id);
            }
        }

        public string GetCardListName(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(l => l.ListId == listId).Select(b => b.Name).FirstOrDefault();
            }
        }

        public int CountCardsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Count(l => l.BoardId == boardId);
            }
        }

        public int CountConditionalCardsOfBoard(Expression<Func<Card, bool>> predicate, int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).Count(l => l.BoardId == boardId);
            }
        }

        public int CountCardsOfList(int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Count(l => l.ListId == listid);
            }
        }

        public int CountConditionalCardsOfList(Expression<Func<Card, bool>> predicate, int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(predicate).Count(l => l.ListId == listid);
            }
        }

        #endregion

        #region Async Methods
        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.ToListAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetAllPagingAsync<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                        await db.Card.Where(b => b.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                       : await db.Card.Where(b => b.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
                }
                return direction == SortDirection.Ascending ?
                    await db.Card.OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                  : await db.Card.OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
            }
        }

        public async Task<Card> GetSingleAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.FirstOrDefaultAsync(c => c.CardId == id);
            }
        }

        public async Task<Card> FindByAsync(Expression<Func<Card, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(predicate).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<Card>> FindAllByAsync(Expression<Func<Card, bool>> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(predicate).ToListAsync();
            }
        }

        public async Task<Card> AddAsync(Card card)
        {
            using (var db = new TrelloModelDBContainer())
            {
                card.Cix = db.Card.Count(ca => ca.ListId == card.ListId) + 1;
                card.CreationDate = DateTime.Now;
                db.Card.Add(card);
                await db.SaveChangesAsync();
            }
            return card;
        }

        public async Task AddRangeAsync(IEnumerable<Card> cards)
        {
            using (var db = new TrelloModelDBContainer())
            {
                var i = 1;
                foreach (var c in cards)
                {
                    c.Cix = db.Card.Count(ca => ca.ListId == c.ListId) + i;
                    c.CreationDate = DateTime.Now;
                    db.Card.Add(c);
                    i++;
                }
                await db.SaveChangesAsync();
            }
        }

        public Task DeleteAsync(Card t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(Card t)
        {
            throw new NotImplementedException();
        }

        public Task EditRangeAsync(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(c => c.BoardId == boardId).ToListAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetCardsOfBoardPagingAsync<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction, string searchString, int pagenumber,
            int pagesize, int boardid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                            await db.Card.Where(c => c.BoardId == boardid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                           : await db.Card.Where(c => c.BoardId == boardid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
                }
                return direction == SortDirection.Ascending ?
                         await db.Card.Where(c => c.BoardId == boardid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                       : await db.Card.Where(c => c.BoardId == boardid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetCardsOfListAsync(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(c => c.ListId == listId).ToListAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetCardsOfListPagingAsync<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize,
            int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return direction == SortDirection.Ascending ?
                           await db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                           : await db.Card.Where(c => c.ListId == listid && c.Name.Contains(searchString)).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
                }
                return direction == SortDirection.Ascending ?
                        await db.Card.Where(c => c.ListId == listid).OrderBy(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync()
                       : await db.Card.Where(c => c.ListId == listid).OrderByDescending(sorter).Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.CountAsync();
            }
        }

        public async Task<bool> ValidIdAsync(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.AnyAsync(c => c.CardId == id);
            }
        }

        public async Task<string> GetCardListNameAsync(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.List.Where(l => l.ListId == listId).Select(b => b.Name).FirstOrDefaultAsync();
            }
        }

        public async Task<int> CountCardsOfBoardAsyn(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.CountAsync(l => l.BoardId == boardId);
            }
        }

        public async Task<int> CountConditionalCardsOfBoardAsyn(Expression<Func<Card, bool>> predicate, int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(predicate).CountAsync(l => l.BoardId == boardId);
            }
        }

        public async Task<int> CountCardsOfListAsyn(int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.CountAsync(l => l.ListId == listid);
            }
        }

        public async Task<int> CountConditionalCardsOfListAsync(Expression<Func<Card, bool>> predicate, int listid)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return await db.Card.Where(predicate).CountAsync(l => l.ListId == listid);
            }
        }
        #endregion
    }
}