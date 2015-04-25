using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Interfaces.Repositories;

namespace TrelloModel.Repository.MongoDB
{
    public class CardRepositoryMongoDB : ICardRepository
    {
        #region Variables and Properties
        private static readonly Lazy<CardRepositoryMongoDB> BoardRepo = new Lazy<CardRepositoryMongoDB>(() => new CardRepositoryMongoDB());

        public static CardRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        public IEnumerable<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetAllPaging(string searchString, int pagenumber, int pagesize)
        {
            throw new NotImplementedException();
        }

        public Card GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Card FindBy(Expression<Func<Card, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> FindAllBy(Expression<Func<Card, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Card t)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Card t)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
        }

        public void Edit(Card t)
        {
            throw new NotImplementedException();
        }

        public void EditRange(IEnumerable<Card> t)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool ValidId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsOfBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsOfList(int listId)
        {
            throw new NotImplementedException();
        }

        public string GetCardListName(int listId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}