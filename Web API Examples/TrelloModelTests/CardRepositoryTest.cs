using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;
using TSC = TrelloModel.Business.Constants.TrelloSizeConstants;

namespace TrelloModelTests
{
    [TestClass]
    public class CardRepositoryTest
    {
        #region Variables and Properties
        private static BoardRepository _br;
        private static ListRepository _lr;
        private static CardRepository _cr;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeCardRepositoryTests(TestContext context)
        {
            _br = (BoardRepository)new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepository();
            _lr = (ListRepository)new RepositoryConcreteFactory().GetListFactory().GetListRepository();
            _cr = (CardRepository)new RepositoryConcreteFactory().GetCardFactory().GetCardRepository();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios and add EditInvalid cases
        #region Invalid Assert
        #endregion

        //TODO Add Tests: EditCard inferior and superior Index, and DeleteCard to see if index of other changes
        #region  Valid Assert
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
            Assert.IsNotNull(_cr.FindAllBy(l => l.CardId == 1));
        }

        [TestMethod]
        public void TestAddDeleteCard()
        {
            var card = new Card
            {
                Name = "Card PI Teste v1",
                Cix = 1,
                BoardId = 1,
                ListId = 1,
                Discription = "Card para teste",
                DueDate = DateTime.Now.AddDays(1)
            };
            _cr.Add(card);
            var addedCard = _cr.FindAllBy(l => l.Name == card.Name).FirstOrDefault();
            Assert.IsNotNull(addedCard);
            Card del = _cr.GetSingle(addedCard.CardId);
            _cr.Delete(del);
            Assert.AreEqual(0, _cr.FindAllBy(l => l.Name == addedCard.Name).Count());
        }
        
        [TestMethod]
        public void TestEditCard()
        {
            var card = _cr.GetSingle(1);
            var ecard = new Card
            {
                CardId = card.CardId,
                Name = "Card Test",
                Cix = 1,
                BoardId = 1,
                ListId = 1,
                Discription = "Card para teste",
                DueDate = DateTime.Now
            };
            _cr.Edit(ecard);
            card = _cr.GetSingle(1);
            Assert.AreEqual(card.Name, ecard.Name);
            Assert.AreEqual(card.CardId, ecard.CardId);
        }
        #endregion
        #endregion
    }
}