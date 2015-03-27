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
        private static BoardRepository _br;
        private static ListRepository _lr;
        private static CardRepository _cr;

        [ClassInitialize]
        public static void ClassInitializeCardRepositoryTests(TestContext context)
        {
            _br = (BoardRepository) new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepository();
            _lr = (ListRepository)  new RepositoryConcreteFactory().GetListFactory().GetListRepository();
            _cr = (CardRepository)  new RepositoryConcreteFactory().GetCardFactory().GetCardRepository();
        }

        [TestMethod]
        public void TestGetAllCards()
        {
            Assert.AreEqual(27, _cr.GetAll().Count());
        }

        [TestMethod]
        public void TestGetSingleCard()
        {
            Assert.IsNotNull(_cr.GetSingle(1));
        }

        [TestMethod]
        public void TestFindCardBy()
        {
            Assert.IsNotNull(_cr.FindBy(l => l.CardId == 1));
        }

        [TestMethod]
        public void TestAddDeleteCard()
        {
            var card = new Card { Name = "Card PI Teste", };
            _cr.Add(card);
            var addedCard = _cr.FindBy(l => l.Name == card.Name).FirstOrDefault();
            Assert.IsNotNull(addedCard);
            Card del = _cr.GetSingle(addedCard.CardId);
            _cr.Delete(del);
            Assert.IsNull(_cr.FindBy(l => l.Name == addedCard.Name));
        }

        [TestMethod]
        public void TestEditCard()
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