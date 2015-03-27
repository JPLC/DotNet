﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrelloModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TrelloModelDBContainer : DbContext
    {
        public TrelloModelDBContainer()
            : base("name=TrelloModelDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Card> Card { get; set; }
    
        public virtual ObjectResult<ProcedureTest_Result> ProcedureTest(Nullable<int> param1, Nullable<int> param2)
        {
            var param1Parameter = param1.HasValue ?
                new ObjectParameter("param1", param1) :
                new ObjectParameter("param1", typeof(int));
    
            var param2Parameter = param2.HasValue ?
                new ObjectParameter("param2", param2) :
                new ObjectParameter("param2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProcedureTest_Result>("ProcedureTest", param1Parameter, param2Parameter);
        }
    
        public virtual int DeleteList(Nullable<int> listId, Nullable<int> lix, Nullable<int> boardId)
        {
            var listIdParameter = listId.HasValue ?
                new ObjectParameter("ListId", listId) :
                new ObjectParameter("ListId", typeof(int));
    
            var lixParameter = lix.HasValue ?
                new ObjectParameter("Lix", lix) :
                new ObjectParameter("Lix", typeof(int));
    
            var boardIdParameter = boardId.HasValue ?
                new ObjectParameter("BoardId", boardId) :
                new ObjectParameter("BoardId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteList", listIdParameter, lixParameter, boardIdParameter);
        }
    
        public virtual int EditList(Nullable<int> listId, Nullable<int> lix, Nullable<int> boardId, string name)
        {
            var listIdParameter = listId.HasValue ?
                new ObjectParameter("ListId", listId) :
                new ObjectParameter("ListId", typeof(int));
    
            var lixParameter = lix.HasValue ?
                new ObjectParameter("Lix", lix) :
                new ObjectParameter("Lix", typeof(int));
    
            var boardIdParameter = boardId.HasValue ?
                new ObjectParameter("BoardId", boardId) :
                new ObjectParameter("BoardId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditList", listIdParameter, lixParameter, boardIdParameter, nameParameter);
        }
    
        public virtual int DeleteCard(Nullable<int> cardId, Nullable<int> cix, Nullable<int> listId)
        {
            var cardIdParameter = cardId.HasValue ?
                new ObjectParameter("CardId", cardId) :
                new ObjectParameter("CardId", typeof(int));
    
            var cixParameter = cix.HasValue ?
                new ObjectParameter("Cix", cix) :
                new ObjectParameter("Cix", typeof(int));
    
            var listIdParameter = listId.HasValue ?
                new ObjectParameter("ListId", listId) :
                new ObjectParameter("ListId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCard", cardIdParameter, cixParameter, listIdParameter);
        }
    
        public virtual int EditCard(Nullable<int> cardId, Nullable<int> cix, string name, string discription, Nullable<System.TimeSpan> creationDate, Nullable<System.TimeSpan> dueDate, Nullable<int> listId)
        {
            var cardIdParameter = cardId.HasValue ?
                new ObjectParameter("CardId", cardId) :
                new ObjectParameter("CardId", typeof(int));
    
            var cixParameter = cix.HasValue ?
                new ObjectParameter("Cix", cix) :
                new ObjectParameter("Cix", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var discriptionParameter = discription != null ?
                new ObjectParameter("Discription", discription) :
                new ObjectParameter("Discription", typeof(string));
    
            var creationDateParameter = creationDate.HasValue ?
                new ObjectParameter("CreationDate", creationDate) :
                new ObjectParameter("CreationDate", typeof(System.TimeSpan));
    
            var dueDateParameter = dueDate.HasValue ?
                new ObjectParameter("DueDate", dueDate) :
                new ObjectParameter("DueDate", typeof(System.TimeSpan));
    
            var listIdParameter = listId.HasValue ?
                new ObjectParameter("ListId", listId) :
                new ObjectParameter("ListId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditCard", cardIdParameter, cixParameter, nameParameter, discriptionParameter, creationDateParameter, dueDateParameter, listIdParameter);
        }
    }
}
