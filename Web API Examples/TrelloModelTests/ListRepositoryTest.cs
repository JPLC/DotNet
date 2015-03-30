using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;

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
        #region Invalid Assert
        #endregion

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
            Assert.IsNotNull(_lr.FindBy(l => l.ListId == 1));
        }

        [TestMethod]
        public void TestAddDeleteList()
        {
            var list = new List { Name = "List PI Teste", BoardId = 1 };
            _lr.Add(list);
            var addedlist = _lr.FindBy(l => l.Name == list.Name).FirstOrDefault();
            Assert.IsNotNull(addedlist);
            List del = _lr.GetSingle(addedlist.ListId);
            _lr.Delete(del);
            Assert.AreEqual(0, _lr.FindBy(l2 => l2.Name == addedlist.Name).Count());
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
        #endregion
        #endregion
    }
}