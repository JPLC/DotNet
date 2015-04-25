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

        public IEnumerable<Card> GetCardsOfList(int listId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.Card.Where(c => c.ListId == listId).ToList();
            }
        }

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
        #endregion
        
        #region Async Methods
        public Task<IEnumerable<Card>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Card>> GetAllPagingAsync(Expression<Func<Card, object>> sorter, SortDirection direction, string searchString, int pagenumber, int pagesize)
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

        public Task<Card> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Card> FindByAsync(Expression<Func<Card, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Card>> FindAllByAsync(Expression<Func<Card, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Card> AddAsync(Card t)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<Card>> GetCardsOfBoardAsync(int boardId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Card>> GetCardsOfListAsync(int listId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
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
        #endregion
    }
}