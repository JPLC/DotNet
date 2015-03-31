﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class ListRepository : IRepository<List>
    {
        #region Variables and Properties
        private static readonly Lazy<ListRepository> ListRepo = new Lazy<ListRepository>(() => new ListRepository());

        public static ListRepository Instance { get { return ListRepo.Value; } }
        #endregion

        #region Constructor
        private ListRepository() { }
        #endregion

        //TODO aplicar e melhorar expection handling
        #region Methods
        private IQueryable<List> GetAllPrivate()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List;
            }
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
            return GetAllPrivate().FirstOrDefault(l => l.ListId == id);
        }

        public List FindBy(Func<List, bool> predicate)
        {
            return GetAllPrivate().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<List> FindAllBy(Func<List, bool> predicate)
        {
            return GetAllPrivate().Where(predicate);
        }

        public void Add(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                list.Lix = db.List.Count() + 1;
                db.List.Add(list);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<List> lists)
        {
            using (var db = new TrelloModelDBContainer())
            {
                foreach (var l in lists)
                {
                    l.Lix = db.List.Count() + 1;
                    db.List.Add(l);
                }
                db.SaveChanges();
            }
        }

        public void Delete(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.DeleteList(list.ListId, list.Lix, list.BoardId);
            }
        }

        public void DeleteRange(IEnumerable<List> lists)
        {
            foreach (var l in lists)
            {
                Delete(l);
            }
        }

        public void Edit(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                db.EditList(list.ListId, list.Lix, list.BoardId, list.Name);
            }
        }
        #endregion
    }
}