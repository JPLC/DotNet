using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;

namespace TrelloModelTests
{
    [TestClass]
    public class CardRepositoryTest
    {
        private static CardRepository _cr;

        [ClassInitialize]
        public static void ClassInitialize_ListRepositoryTests(TestContext context)
        {
            _cr = (CardRepository)new RepositoryConcreteFactory().GetCardFactory().GetCardRepository();
        }

        [TestMethod]
        public void TestGetAllLists()
        {
            //Assert.AreEqual(1,br.GetAll().Count());
        }

        [TestMethod]
        public void TestGetSingleList()
        {
            Assert.IsNotNull(_cr.GetSingle(1));
        }

        [TestMethod]
        public void TestFindListBy()
        {
            Assert.IsNotNull(_cr.FindBy(l => l.ListId == 1));
        }

        [TestMethod]
        public void TestAddDeleteList()
        {
            var card = new Card { Name = "Card PI Teste", Cix = 1 };
            _cr.Add(card);
            var addedlist = _cr.FindBy(l => l.Name == card.Name).FirstOrDefault();
            Assert.IsNotNull(addedlist);
            Card del = _cr.GetSingle(addedlist.ListId);
            _cr.Delete(del);
            Assert.IsNull(_cr.FindBy(l => l.Name == addedlist.Name));
        }

        [TestMethod]
        public void TestEditList()
        {
            var card = _cr.GetSingle(1);
            var ecard = new Card { CardId = card.CardId, Name = "", Cix = 1 };
            _cr.Edit(ecard);
            card = _cr.GetSingle(1);
            Assert.Equals(card.Name, ecard.Name);
            Assert.Equals(card.CardId, ecard.CardId);
        }
    }
}