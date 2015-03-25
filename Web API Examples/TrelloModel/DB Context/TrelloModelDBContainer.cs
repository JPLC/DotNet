using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace TrelloModel
{
    public partial class TrelloModelDBContainer
    {
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            //TODO Acabar as validações do modelo de dados
            if (entityEntry.Entity is Board)
            {
                if (entityEntry.CurrentValues.GetValue<string>("Name") == string.Empty)
                {
                    var list = new List<DbValidationError>();
                    list.Add(new DbValidationError("Name", "Board Name is required"));

                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            if (entityEntry.Entity is List)
            {
                if (entityEntry.CurrentValues.GetValue<string>("Name") == string.Empty)
                {
                    var list = new List<DbValidationError>();
                    list.Add(new DbValidationError("Name", "List Name is required"));

                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            if (entityEntry.Entity is Card)
            {
                if (entityEntry.CurrentValues.GetValue<string>("Name") == string.Empty)
                {
                    var list = new List<DbValidationError>();
                    list.Add(new DbValidationError("Name", "Card Name is required"));

                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
