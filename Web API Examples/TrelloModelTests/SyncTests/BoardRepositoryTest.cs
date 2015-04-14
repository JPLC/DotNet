using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository.SQL;
using TSC = TrelloModel.Business.Constants.TrelloSizeConstants;

namespace TrelloModelTests
{
    [TestClass]
    public class BoardRepositoryTest
    {
        #region Variables and Properties
        private static BoardRepositorySQL _br;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeBoardRepositoryTests(TestContext context)
        {
            _br = (BoardRepositorySQL)new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepositorySQL();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios
        #region Invalid Assert
        [TestMethod]
        public void TestAddInvalidBoardNull()
        {
            var board = new Board { Name = null, Discription = null };
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardEmpty()
        {
            var board = new Board { Name = string.Empty, Discription = string.Empty };
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardBiggerThanMaxValue()
        {
            var board = new Board { Name = new String('a', TSC.BoardNameSize + 1), Discription = new String('a', TSC.BoardDiscriptionSize + 1) };
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardInvalidChars()
        {
            var board = new Board { Name = "#$%@£@erfnerio", Discription = "#$%@£@erfnerio" };
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardSameName()
        {
            var uniqueboard = _br.GetSingle(1);
            var board = new Board { Name = uniqueboard.Name, Discription = "teste repeated board" };
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardNull()
        {
            var board = _br.GetSingle(1);
            board.Name = null; board.Discription = null;
            try
            {
                _br.Edit(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardEmpty()
        {
            var board = _br.GetSingle(1);
            board.Name = string.Empty; board.Discription = string.Empty;
            try
            {
                _br.Edit(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardBiggerThanMaxValue()
        {
            var board = _br.GetSingle(1);
            board.Name = new String('a', TSC.BoardNameSize + 1); board.Discription = new String('a', TSC.BoardDiscriptionSize + 1);
            try
            {
                _br.Edit(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardInvalidChars()
        {
            var board = _br.GetSingle(1);
            board.Name = "#$%@£@erfnerio"; board.Discription = "#$%@£@erfnerio";
            try
            {
                _br.Edit(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardSameName()
        {
            var uniqueboard = _br.GetSingle(2);
            var board = _br.GetSingle(1);
            board.Name = uniqueboard.Name; board.Discription = "teste repeated board";
            try
            {
                _br.Add(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }
        #endregion

        #region  Valid Assert
        [TestMethod]
        public void TestGetAllBoards()
        {
            var b = _br.GetAll();
            var count = b.Count();
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestCountAllBoards()
        {
            Assert.AreEqual(3, _br.Count());
        }

        [TestMethod]
        public void TestGetSingleBoard()
        {
            var board = _br.GetSingle(1);
            Assert.IsNotNull(board);
            Assert.AreEqual(1, board.BoardId);
            Assert.AreEqual("Caetano", board.Name);
            Assert.AreEqual("Quadro do Caetano", board.Discription);
        }

        [TestMethod]
        public void TestFindBoardBy()
        {
            Assert.IsNotNull(_br.FindAllBy(b => b.BoardId == 1));
            Assert.IsNotNull(_br.FindAllBy(b => b.Name == "Caetano"));
            Assert.IsNotNull(_br.FindAllBy(b => b.Discription == "Quadro do Caetano"));
        }

        [TestMethod]
        public void TestAddDeleteBoard()
        {
            var startNBoards = _br.Count();
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet Teste" };
            _br.Add(board);
            Assert.AreEqual(startNBoards + 1, _br.Count());
            var addedboard = _br.FindAllBy(b => b.Name == board.Name).FirstOrDefault();
            Assert.IsNotNull(addedboard);
            _br.Delete(addedboard);
            Assert.AreEqual(0, _br.FindAllBy(b => b.Name == addedboard.Name).Count());
            Assert.AreEqual(startNBoards, _br.Count());
        }

        [TestMethod]
        public void TestAddDeleteRangeBoard()
        {
            const string desc = "Board PI TestAddDeleteRangeBoard";
            var startNBoards = _br.Count();
            var boardlist = new List<Board>()
                            {
                                new Board {Name = "Board PI TestAddDeleteRange1", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange2", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange3", Discription = desc}
                            };
            _br.AddRange(boardlist);
            Assert.AreEqual(startNBoards + 3, _br.Count());
            var addedboards = _br.FindAllBy(b => b.Discription == desc);
            Assert.AreEqual(3,addedboards.Count());
            _br.DeleteRange(addedboards);
            Assert.AreEqual(0, _br.FindAllBy(b => b.Discription == desc).Count());
            Assert.AreEqual(startNBoards, _br.Count());
        }

        [TestMethod]
        public void TestEditBoard()
        {
            var board = _br.GetSingle(2);
            var eboard = new Board { BoardId = board.BoardId, Name = "Board Edited", Discription = "Board usado para editar" };
            _br.Edit(eboard);
            board = _br.GetSingle(2);
            Assert.AreEqual(board.Name, eboard.Name);
            Assert.AreEqual(board.Discription, eboard.Discription);
            Assert.AreEqual(board.BoardId, eboard.BoardId);
            eboard = new Board { BoardId = board.BoardId, Name = "Pedro", Discription = "Quadro do Pedro" };
            _br.Edit(eboard);
            board = _br.GetSingle(2);
            Assert.AreEqual(board.Name, eboard.Name);
            Assert.AreEqual(board.Discription, eboard.Discription);
            Assert.AreEqual(board.BoardId, eboard.BoardId);
        }

        [TestMethod]
        public void TestEditRangeBoard()
        {
            var eboardlist = new List<Board>()
                         {
                             new Board {BoardId = 1, Name = "Board Edited1", Discription = "Board usado para editar"},
                             new Board {BoardId = 2, Name = "Board Edited2", Discription = "Board usado para editar"},
                             new Board {BoardId = 3, Name = "Board Edited3", Discription = "Board usado para editar"}
                         };
            _br.EditRange(eboardlist);
            var board = _br.GetSingle(1);
            Assert.AreEqual(board.BoardId, 1);
            Assert.AreEqual(board.Name, "Board Edited1");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            board = _br.GetSingle(2);
            Assert.AreEqual(board.BoardId, 2);
            Assert.AreEqual(board.Name, "Board Edited2");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            board = _br.GetSingle(3);
            Assert.AreEqual(board.BoardId, 3);
            Assert.AreEqual(board.Name, "Board Edited3");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            eboardlist = new List<Board>()
                         {
                             new Board {BoardId = 1, Name = "Caetano", Discription = "Quadro do Caetano"},
                             new Board {BoardId = 2, Name = "Pedro", Discription = "Quadro do Pedro"},
                             new Board {BoardId = 3, Name = "Joao", Discription = "Quadro do Joao"}
                         };
            _br.EditRange(eboardlist);
            board = _br.GetSingle(1);
            Assert.AreEqual(board.BoardId, 1);
            Assert.AreEqual(board.Name, "Caetano");
            Assert.AreEqual(board.Discription, "Quadro do Caetano");
            board = _br.GetSingle(2);
            Assert.AreEqual(board.BoardId, 2);
            Assert.AreEqual(board.Name, "Pedro");
            Assert.AreEqual(board.Discription, "Quadro do Pedro");
            board = _br.GetSingle(3);
            Assert.AreEqual(board.BoardId, 3);
            Assert.AreEqual(board.Name, "Joao");
            Assert.AreEqual(board.Discription, "Quadro do Joao");
        }
        #endregion
        #endregion
    }
}