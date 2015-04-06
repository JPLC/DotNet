using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository.SQL;
using TSC = TrelloModel.Business.Constants.TrelloSizeConstants;

namespace TrelloModelTests
{
    [TestClass]
    public class CardRepositoryAsyncTest
    {
        #region Variables and Properties
        private static CardRepositorySQL _cr;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeCardRepositoryTests(TestContext context)
        {
            _cr = (CardRepositorySQL)new RepositoryConcreteFactory().GetCardFactory().GetCardRepositorySQL();
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
                Assert.IsTrue(ex is DbEntityValidationException);
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

        [TestMethod]
        public void TestEditRangeCard()
        {
            var ecardlist = new List<Card>
            {
                             new Card {CardId = 1, ListId = 1, BoardId = 1, Cix = 3, Name = "Card Edited1", Discription = "Card usado para editar", DueDate = DateTime.Now.AddDays(1)},
                             new Card {CardId = 2, ListId = 1, BoardId = 1, Cix = 2, Name = "Card Edited2", Discription = "Card usado para editar", DueDate = DateTime.Now.AddDays(1)},
                             new Card {CardId = 3, ListId = 1, BoardId = 1, Cix = 1, Name = "Card Edited3", Discription = "Card usado para editar", DueDate = DateTime.Now.AddDays(1)}
                         };
            _cr.EditRange(ecardlist);
            var list = _cr.GetSingle(1);
            Assert.AreEqual(list.CardId, 1);
            Assert.AreEqual(list.Name, "Card Edited1");
            Assert.AreEqual(list.Discription, "Card usado para editar");
            Assert.AreEqual(list.Cix, 3);
            list = _cr.GetSingle(2);
            Assert.AreEqual(list.CardId, 2);
            Assert.AreEqual(list.Name, "Card Edited2");
            Assert.AreEqual(list.Discription, "Card usado para editar");
            Assert.AreEqual(list.Cix, 2);
            list = _cr.GetSingle(3);
            Assert.AreEqual(list.CardId, 3);
            Assert.AreEqual(list.Name, "Card Edited3");
            Assert.AreEqual(list.Discription, "Card usado para editar");
            Assert.AreEqual(list.Cix, 1);
            ecardlist = new List<Card>()
                         {
                             new Card {CardId = 1, Cix = 1, ListId = 1, BoardId = 1, Name = "Etapa1", Discription = "Primeira etapa de LS", DueDate = DateTime.Now.AddDays(1)},
                             new Card {CardId = 2, Cix = 2, ListId = 1, BoardId = 1, Name = "Etapa2", Discription = "Segunda etapa de LS", DueDate = DateTime.Now.AddDays(1)},
                             new Card {CardId = 3, Cix = 3, ListId = 1, BoardId = 1, Name = "Etapa3", Discription = "Terceira etapa de LS", DueDate = DateTime.Now.AddDays(1)}
                         };
            _cr.EditRange(ecardlist);
            list = _cr.GetSingle(1);
            Assert.AreEqual(list.CardId, 1);
            Assert.AreEqual(list.Name, "Etapa1");
            Assert.AreEqual(list.Discription, "Primeira etapa de LS");
            Assert.AreEqual(list.Cix, 1);
            list = _cr.GetSingle(2);
            Assert.AreEqual(list.CardId, 2);
            Assert.AreEqual(list.Name, "Etapa2");
            Assert.AreEqual(list.Discription, "Segunda etapa de LS");
            Assert.AreEqual(list.Cix, 2);
            list = _cr.GetSingle(3);
            Assert.AreEqual(list.CardId, 3);
            Assert.AreEqual(list.Name, "Etapa3");
            Assert.AreEqual(list.Discription, "Terceira etapa de LS");
            Assert.AreEqual(list.Cix, 3);
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