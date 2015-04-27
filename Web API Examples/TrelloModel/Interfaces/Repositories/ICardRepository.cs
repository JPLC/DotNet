using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrelloModel.Repository;

namespace TrelloModel.Interfaces.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        IEnumerable<Card> GetCardsOfBoard(int boardId);

        IEnumerable<Card> GetCardsOfBoardPaging<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int boardid);

        IEnumerable<Card> GetCardsOfList(int listId);

        IEnumerable<Card> GetCardsOfListPaging<TKey>(Expression<Func<Card, TKey>> sorter, SortDirection direction,
        string searchString, int pagenumber, int pagesize, int listid);

        string GetCardListName(int listId);

        int CountCardsOfBoard(int boardId);

        int CountConditionalCardsOfBoard(Expression<Func<Card, bool>> predicate, int boardId);

        int CountCardsOfList(int listid);

        int CountConditionalCardsOfList(Expression<Func<Card, bool>> predicate, int listid);
    }
}