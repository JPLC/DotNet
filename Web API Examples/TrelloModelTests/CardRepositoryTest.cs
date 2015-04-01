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
    public class CardRepositoryTest
    {
        #region Variables and Properties
        private static CardRepository _cr;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeCardRepositoryTests(TestContext context)
        {
            _cr = (CardRepository)new RepositoryConcreteFactory().GetCardFactory().GetCardRepository();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios
        #region Invalid Assert
        [TestMethod]
        public void TestAddInvalidCardNull()
        {
            var card = new Card { Name = null, Discription = null };
            try
            {
                _cr.Add(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidCardEmpty()
        {
            var card = new Card { Name = string.Empty, Discription = string.Empty };
            try
            {
                _cr.Add(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidCardBiggerThanMaxValue()
        {
            var card = new Card { Name = new String('a', TSC.CardNameSize + 1), Discription = new String('a', TSC.CardDiscriptionSize + 1) };
            try
            {
                _cr.Add(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidCardInvalidChars()
        {
            var card = new Card { Name = "#$%@£@erfnerio", Discription = "#$%@£@erfnerio" };
            try
            {
                _cr.Add(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidCardNull()
        {
            var card = _cr.GetSingle(1);
            card.Name = null; card.Discription = null;
            try
            {
                _cr.Edit(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex != null);
            }
        }

        [TestMethod]
        public void TestEditInvalidCardEmpty()
        {
            var card = _cr.GetSingle(1);
            card.Name = string.Empty; card.Discription = string.Empty;
            try
            {
                _cr.Edit(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidCardBiggerThanMaxValue()
        {
            var card = _cr.GetSingle(1);
            card.Name = new String('a', TSC.CardNameSize + 1); card.Discription = new String('a', TSC.CardDiscriptionSize + 1);
            try
            {
                _cr.Edit(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidCardInvalidChars()
        {
            var card = _cr.GetSingle(1);
            card.Name = "#$%@£@erfnerio"; card.Discription = "#$%@£@erfnerio";
            try
            {
                _cr.Edit(card);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }
        #endregion

        #region  Valid Assert
        [TestMethod]
        public void TestGetAllCards()
        {
            var c = _cr.GetAll();
            var count = c.Count();
            Assert.AreEqual(27, count);
        }

        [TestMethod]
        public void TestCountAllCards()
        {
            Assert.AreEqual(27, _cr.Count());
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
        public void TestAddDeleteRangeCard()
        {
            var cards = new List<Card>
            {
                new Card{Name = "Card PI Teste1", BoardId = 1, ListId = 1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste2", BoardId = 1, ListId = 1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste3", BoardId = 1, ListId = 1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste4", BoardId = 1, ListId = 1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)}
            };
            _cr.AddRange(cards);
            Assert.AreEqual(7, _cr.GetCardsOfList(1).Count());
            _cr.DeleteRange(cards);
            Assert.AreEqual(3, _cr.GetCardsOfList(1).Count());
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

        /*TODO TestEditRangeCard*/
        [TestMethod]
        public void TestEditRangeCard()
        {
        }
       
        [TestMethod]
        public void TestEditCardFromSuperiorIndex()
        {
            var countinit = _cr.Count();
            var lindex = _cr.GetCardsOfList(1).Count();
            var cards = new List<Card>
            {
                new Card{Name = "Card PI Teste1", BoardId = 1, ListId = 1, Cix = lindex+1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste2", BoardId = 1, ListId = 1, Cix = lindex+2, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste3", BoardId = 1, ListId = 1, Cix = lindex+3, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste4", BoardId = 1, ListId = 1, Cix = lindex+4, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)}
            };
            _cr.AddRange(cards);
            _cr.Count().Should().Be(countinit + cards.Count);
            var listtoedit = _cr.FindBy(l => l.Name == "Card PI Teste4");
            listtoedit.Cix = lindex + 2;
            _cr.Edit(listtoedit);
            _cr.FindBy(c => c.Name == "Card PI Teste1").Cix.Should().Be(lindex + 1);
            _cr.FindBy(c => c.Name == "Card PI Teste2").Cix.Should().Be(lindex + 3);
            _cr.FindBy(c => c.Name == "Card PI Teste3").Cix.Should().Be(lindex + 4);
            _cr.DeleteRange(cards);
            _cr.Count().Should().Be(countinit);
        }
              
        [TestMethod]
        public void TestEditCardFromInferiorIndex()
        {
            var countinit = _cr.Count();
            var lindex = _cr.GetCardsOfList(1).Count();
            var cards = new List<Card>
            {
                new Card{Name = "Card PI Teste1", BoardId = 1, ListId = 1, Cix = lindex+1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste2", BoardId = 1, ListId = 1, Cix = lindex+2, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste3", BoardId = 1, ListId = 1, Cix = lindex+3, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste4", BoardId = 1, ListId = 1, Cix = lindex+4, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)}
            };
            _cr.AddRange(cards);
            _cr.Count().Should().Be(countinit + cards.Count);
            var listtoedit = _cr.FindBy(l => l.Name == "Card PI Teste1");
            listtoedit.Cix = lindex + 4;
            _cr.Edit(listtoedit);
            _cr.FindBy(l => l.Name == "Card PI Teste2").Cix.Should().Be(lindex + 1);
            _cr.FindBy(l => l.Name == "Card PI Teste3").Cix.Should().Be(lindex + 2);
            _cr.FindBy(l => l.Name == "Card PI Teste4").Cix.Should().Be(lindex + 3);
            _cr.DeleteRange(cards);
            _cr.Count().Should().Be(countinit);
        }
        
        [TestMethod]
        public void TestRemoveCardIndex()
        {
            var countinit = _cr.Count();
            var lindex = _cr.GetCardsOfList(1).Count();
            var cards = new List<Card>
            {
                new Card{Name = "Card PI Teste1", BoardId = 1, ListId = 1, Cix = lindex+1, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste2", BoardId = 1, ListId = 1, Cix = lindex+2, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste3", BoardId = 1, ListId = 1, Cix = lindex+3, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)},
                new Card{Name = "Card PI Teste4", BoardId = 1, ListId = 1, Cix = lindex+4, Discription = "Teste", DueDate = DateTime.Now.AddDays(1)}
            };
            _cr.AddRange(cards);
            _cr.Count().Should().Be(countinit + cards.Count);
            _cr.Delete(_cr.FindBy(c => c.Name == "Card PI Teste3"));
            _cr.FindBy(c => c.Name == "Card PI Teste1").Cix.Should().Be(lindex + 1);
            _cr.FindBy(c => c.Name == "Card PI Teste2").Cix.Should().Be(lindex + 2);
            _cr.FindBy(c => c.Name == "Card PI Teste4").Cix.Should().Be(lindex + 3);
            _cr.DeleteRange(cards);
            _cr.Count().Should().Be(countinit);
        }

        [TestMethod]
        public void TestGetCardsOfBoard()
        {
            Assert.AreEqual(9, _cr.GetCardsOfBoard(1).Count());
        }

        [TestMethod]
        public void TestGetCardsOfList()
        {
            Assert.AreEqual(3, _cr.GetCardsOfList(1).Count());
        }
        #endregion
        #endregion
    }
}