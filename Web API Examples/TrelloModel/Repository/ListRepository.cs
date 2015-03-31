﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrelloModel.Interfaces;

namespace TrelloModel.Repository
{
    public class ListRepository : IListRepository
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
        public IEnumerable<List> GetAll()
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.ToList();
            }
        }

        public List GetSingle(int id)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.FirstOrDefault(l => l.ListId == id);
            }
        }

        public List FindBy(Func<List, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<List> FindAllBy(Func<List, bool> predicate)
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Where(predicate).ToList();
            }
        }

        public void Add(List list)
        {
            using (var db = new TrelloModelDBContainer())
            {
                list.Lix = db.List.Count(li => li.BoardId == list.BoardId) + 1;
                db.List.Add(list);
                db.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<List> lists)
        {
            using (var db = new TrelloModelDBContainer())
            {
                int i = 1;
                foreach (var l in lists)
                {
                    l.Lix = db.List.Count(li => li.BoardId == l.BoardId) + i;
                    db.List.Add(l);
                    i++;
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

        public void EditRange(IEnumerable<List> lists)
        {
            foreach (var list in lists)
            {
                Edit(list);
            }
        }

        public IEnumerable<List> GetListsOfBoard(int boardId)
        {
            using (var db = new TrelloModelDBContainer())
            {
                return db.List.Where(l => l.BoardId == boardId).ToList();
            }
        }

        public int Count()
        {
            using (var db = new TrelloModelDBContainer())
            {
                //db.Database.Log = (msg) => { Console.WriteLine(msg ); };
                return db.List.Count();
            }
        }
        #endregion
    }
}