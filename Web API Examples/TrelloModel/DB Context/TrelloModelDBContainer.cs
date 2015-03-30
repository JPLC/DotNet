using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using TrelloModel.Business;
using TrelloModel.Business.Enumerators;

namespace TrelloModel
{
    public partial class TrelloModelDBContainer
    {
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            items = new Dictionary<object, object>();
            if (entityEntry.Entity is Board)
            {
                List<KeyValuePair<BoardValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
                if (!BoardBusiness.ValidateBoard((Board)entityEntry.Entity, Board.ToList(), out errorMsgDic))
                {
                    var list = new List<DbValidationError>();
                    foreach (var err in errorMsgDic)
                    {                       
                        list.Add(new DbValidationError(err.Value.Key, err.Value.Value));
                        items.Add(err.Key, err.Value);
                    }                  
                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            if (entityEntry.Entity is List)
            {
                List<KeyValuePair<ListValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
                if (!ListBusiness.ValidateList((List)entityEntry.Entity, out errorMsgDic))
                {
                    var list = new List<DbValidationError>();
                    foreach (var err in errorMsgDic)
                    {
                        list.Add(new DbValidationError(err.Value.Key, err.Value.Value));
                        items.Add(err.Key, err.Value);
                    }
                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            if (entityEntry.Entity is Card)
            {
                List<KeyValuePair<CardValidationCodes, KeyValuePair<string, string>>> errorMsgDic;
                if (!CardBusiness.ValidateCard((Card)entityEntry.Entity, out errorMsgDic))
                {
                    var list = new List<DbValidationError>();
                    foreach (var err in errorMsgDic)
                    {
                        list.Add(new DbValidationError(err.Value.Key, err.Value.Value));
                        items.Add(err.Key, err.Value);
                    }
                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}