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
        private static ListRepository _lr;

        [ClassInitialize]
        public static void ClassInitialize_ListRepositoryTests(TestContext context)
        {
            _lr = (ListRepository)new RepositoryConcreteFactory().GetListFactory().GetListRepository();
        }

        [TestMethod]
        public void TestGetAllLists()
        {
            //Assert.AreEqual(1,br.GetAll().Count());
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
            var list = new List { Name = "List PI Teste", Lix = 1 };
            _lr.Add(list);
            var addedlist = _lr.FindBy(l => l.Name == list.Name).FirstOrDefault();
            Assert.IsNotNull(addedlist);
            List del = _lr.GetSingle(addedlist.ListId);
            _lr.Delete(del);
            Assert.IsNull(_lr.FindBy(l => l.Name == addedlist.Name));
        }

        [TestMethod]
        public void TestEditList()
        {
            var list = _lr.GetSingle(1);
            var elist = new List { ListId = list.ListId, Name = "", Lix = 1 };
            _lr.Edit(elist);
            list = _lr.GetSingle(1);
            Assert.Equals(list.Name, elist.Name);
            Assert.Equals(list.ListId, elist.ListId);
        }
    }
}