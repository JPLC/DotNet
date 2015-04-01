using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FluentAssertions;
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
        private static ListRepository _lr;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeListRepositoryTests(TestContext context)
        {
            _lr = (ListRepository)new RepositoryConcreteFactory().GetListFactory().GetListRepository();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios
        #region Invalid Assert
        [TestMethod]
        public void TestAddInvalidListNull()
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
        public void TestAddInvalidListEmpty()
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
        public void TestAddInvalidListBiggerThanMaxValue()
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
        public void TestAddInvalidListInvalidChars()
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

        public void TestEditInvalidListNull()
        {
            var list = _lr.GetSingle(1);
            list.Name = null;
            try
            {
                _lr.Edit(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidListEmpty()
        {
            var list = _lr.GetSingle(1);
            list.Name = string.Empty;
            try
            {
                _lr.Edit(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidListBiggerThanMaxValue()
        {
            var list = _lr.GetSingle(1);
            list.Name = new String('a', TSC.ListNameSize + 1);
            try
            {
                _lr.Edit(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidListInvalidChars()
        {
            var list = _lr.GetSingle(1);           
            list.Name = "#$%@£@erfnerio";
            try
            {
                _lr.Edit(list);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }
        #endregion

        #region  Valid Assert
        [TestMethod]
        public void TestGetAllLists()
        {
            var l = _lr.GetAll();
            var count = l.Count();
            Assert.AreEqual(9, count);
        }

        public void TestCountAllLists()
        {
            Assert.AreEqual(9, _lr.Count());
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
        public void TestAddDeleteRangeList()
        {
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1 },
                new List{Name = "List PI Teste2", BoardId = 1 },
                new List{Name = "List PI Teste3", BoardId = 1 },
                new List{Name = "List PI Teste4", BoardId = 1 }
            };
            _lr.AddRange(list);
            Assert.AreEqual(7, _lr.GetListsOfBoard(1).Count());
            _lr.DeleteRange(list);
            Assert.AreEqual(3, _lr.GetListsOfBoard(1).Count());
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

        /*TODO TestEditRangeList*/
        [TestMethod]
        public void TestEditRangeList()
        {
            /*var list = new List { Name = "List PI Teste", BoardId = 1 };
            _lr.Add(list);
            var elist = new List { ListId = list.ListId, Name = "List PI Teste2", Lix = list.Lix + 1, BoardId = list.BoardId };
            _lr.Edit(elist);
            list = _lr.GetSingle(elist.ListId);
            Assert.AreEqual(list.Name, elist.Name);
            Assert.AreEqual(list.ListId, elist.ListId);
            _lr.Delete(elist);*/
        }

        [TestMethod]
        public void TestEditListFromSuperiorIndex()
        {
            var countinit = _lr.Count();
            var lindex = _lr.GetListsOfBoard(1).Count();
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1, Lix = lindex+1},
                new List{Name = "List PI Teste2", BoardId = 1, Lix = lindex+2},
                new List{Name = "List PI Teste3", BoardId = 1, Lix = lindex+3},
                new List{Name = "List PI Teste4", BoardId = 1, Lix = lindex+4}
            };
            _lr.AddRange(list);
            _lr.Count().Should().Be(countinit + list.Count);
            var listtoedit = _lr.FindBy(l => l.Name == "List PI Teste4");
            listtoedit.Lix = lindex + 2;
            _lr.Edit(listtoedit);
            _lr.FindBy(l => l.Name == "List PI Teste1").Lix.Should().Be(lindex + 1);
            _lr.FindBy(l => l.Name == "List PI Teste2").Lix.Should().Be(lindex + 3);
            _lr.FindBy(l => l.Name == "List PI Teste3").Lix.Should().Be(lindex + 4);
            _lr.DeleteRange(list);
            _lr.Count().Should().Be(countinit);
        }

        [TestMethod]
        public void TestEditListFromInferiorIndex()
        {
            var countinit = _lr.Count();
            var lindex = _lr.GetListsOfBoard(1).Count();
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1, Lix = lindex+1},
                new List{Name = "List PI Teste2", BoardId = 1, Lix = lindex+2},
                new List{Name = "List PI Teste3", BoardId = 1, Lix = lindex+3},
                new List{Name = "List PI Teste4", BoardId = 1, Lix = lindex+4}
            };
            _lr.AddRange(list);
            _lr.Count().Should().Be(countinit + list.Count);
            var listtoedit = _lr.FindBy(l => l.Name == "List PI Teste1");
            listtoedit.Lix = lindex + 4;
            _lr.Edit(listtoedit);
            _lr.FindBy(l => l.Name == "List PI Teste2").Lix.Should().Be(lindex + 1);
            _lr.FindBy(l => l.Name == "List PI Teste3").Lix.Should().Be(lindex + 2);
            _lr.FindBy(l => l.Name == "List PI Teste4").Lix.Should().Be(lindex + 3);
            _lr.DeleteRange(list);
            _lr.Count().Should().Be(countinit);
        }

        [TestMethod]
        public void TestRemoveListIndex()
        {
            var countinit = _lr.Count();
            var lindex = _lr.GetListsOfBoard(1).Count();
            var list = new List<List>
            {
                new List{Name = "List PI Teste1", BoardId = 1, Lix = lindex+1},
                new List{Name = "List PI Teste2", BoardId = 1, Lix = lindex+2},
                new List{Name = "List PI Teste3", BoardId = 1, Lix = lindex+3},
                new List{Name = "List PI Teste4", BoardId = 1, Lix = lindex+4}
            };
            _lr.AddRange(list);
            _lr.Count().Should().Be(countinit + list.Count);
            _lr.Delete(_lr.FindBy(l => l.Name == "List PI Teste3"));
            _lr.FindBy(l => l.Name == "List PI Teste1").Lix.Should().Be(lindex + 1);
            _lr.FindBy(l => l.Name == "List PI Teste2").Lix.Should().Be(lindex + 2);
            _lr.FindBy(l => l.Name == "List PI Teste4").Lix.Should().Be(lindex + 3);
            _lr.DeleteRange(list);
            _lr.Count().Should().Be(countinit);
        }

        [TestMethod]
        public void TestGetListsOfBoard()
        {
            Assert.AreEqual(3,_lr.GetListsOfBoard(1).Count());
        }
        #endregion
        #endregion
    }
}