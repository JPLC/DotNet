using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;
using TSC = TrelloModel.Business.Constants.TrelloSizeConstants;

namespace TrelloModelTests
{
    [TestClass]
    public class ListRepositoryTest
    {
        #region Variables and Properties
        private static BoardRepository _br;
        private static ListRepository _lr;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeListRepositoryTests(TestContext context)
        {
            _br = (BoardRepository)new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepository();
            _lr = (ListRepository)new RepositoryConcreteFactory().GetListFactory().GetListRepository();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios and add EditInvalid cases
        #region Invalid Assert
        [TestMethod]
        public void TestAddInvalidBoardNull()
        {
            var list = new List { Name = null};
            try
            {
                _lr.Add(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardEmpty()
        {
            var list = new List { Name = string.Empty};
            try
            {
                _lr.Add(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardBiggerThanMaxValue()
        {
            var list = new List { Name = new String('a', TSC.ListNameSize + 1)};
            try
            {
                _lr.Add(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardInvalidChars()
        {
            var list = new List { Name = "#$%@£@erfnerio"};
            try
            {
                _lr.Add(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }
        #endregion

        //TODO Add Tests: EditList inferior and superior Index, and DeleteList to see if index of other changes
        #region  Valid Assert
        [TestMethod]
        public void TestGetAllLists()
        {
            Assert.AreEqual(9, _lr.GetAll().Count());
        }

        [TestMethod]
        public void TestGetSingleList()
        {
            Assert.IsNotNull(_lr.GetSingle(1));
        }

        [TestMethod]
        public void TestFindListBy()
        {
            Assert.IsNotNull(_lr.FindAllBy(l => l.ListId == 1));
        }

        [TestMethod]
        public void TestAddDeleteList()
        {
            var list = new List { Name = "List PI Teste", BoardId = 1 };
            _lr.Add(list);
            var addedlist = _lr.FindAllBy(l => l.Name == list.Name).FirstOrDefault();
            Assert.IsNotNull(addedlist);
            List del = _lr.GetSingle(addedlist.ListId);
            _lr.Delete(del);
            Assert.AreEqual(0, _lr.FindAllBy(l2 => l2.Name == addedlist.Name).Count());
        }

        [TestMethod]
        public void TestEditList()
        {
            var list = new List { Name = "List PI Teste", BoardId = 1 };
            _lr.Add(list);
            var elist = new List { ListId = list.ListId, Name = "List PI Teste2", Lix = list.Lix + 1, BoardId = list.BoardId };
            _lr.Edit(elist);
            list = _lr.GetSingle(elist.ListId);
            Assert.AreEqual(list.Name, elist.Name);
            Assert.AreEqual(list.ListId, elist.ListId);
            _lr.Delete(elist);
        }

        [TestMethod]
        public void TestEditListSuperiorIndex()
        {
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1 },
                new List{Name = "List PI Teste2", BoardId = 1 },
                new List{Name = "List PI Teste3", BoardId = 1 },
                new List{Name = "List PI Teste4", BoardId = 1 }
            };
            _lr.AddRange(list);
            var listtodelete = _lr.FindBy(l => l.Name == "List PI Teste4");
            _lr.Delete(listtodelete);

        }

        [TestMethod]
        public void TestEditListInferiorIndex()
        {
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1 },
                new List{Name = "List PI Teste2", BoardId = 1 },
                new List{Name = "List PI Teste3", BoardId = 1 },
                new List{Name = "List PI Teste4", BoardId = 1 }
            };
            _lr.AddRange(list);
            var listtodelete = _lr.FindBy(l => l.Name == "List PI Teste2");
            _lr.Delete(listtodelete);
        }

        [TestMethod]
        public void TestDeleteList()
        {
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1 },
                new List{Name = "List PI Teste2", BoardId = 1 },
                new List{Name = "List PI Teste3", BoardId = 1 },
                new List{Name = "List PI Teste4", BoardId = 1 }
            };
            _lr.AddRange(list);
        }
        #endregion
        #endregion
    }
}