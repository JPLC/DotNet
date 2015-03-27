using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class ListRepository : IRepository<List>
    {
        private static readonly Lazy<ListRepository> ListRepo = new Lazy<ListRepository>(() => new ListRepository());

        public static ListRepository Instance { get { return ListRepo.Value; } }

        private ListRepository() { }

        public IEnumerable<List> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.ToList();
            }
        }

        public List GetSingle(int id)
        {
            return GetAll().FirstOrDefault(l => l.ListId == id);
        }

        public IEnumerable<List> FindBy(Func<List, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Add(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                list.Lix = db.List.Count() + 1;
                db.List.Add(list);
            }
        }

        public void Delete(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteList(list.ListId, list.Lix, list.BoardId);
            }
        }

        public void Edit(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditList(list.ListId, list.Lix, list.BoardId, list.Name);
            }
        }
    }
}