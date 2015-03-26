using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class ListRespository : IRepository<List>
    {

        internal ListRespository()
        {
        }

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

        public void Add(List entity)
        {
            using (var db = new TrelloModelDBContainer())
            {
               
            }
        }

        public void Delete(List entity)
        {
            using (var db = new TrelloModelDBContainer())
            {
                
            }
        }

        public void Edit(List entity)
        {
            using (var db = new TrelloModelDBContainer())
            {

            }
        }
    }
}
