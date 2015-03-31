using System;
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
    public class BoardRepositoryTest
    {
        #region Variables and Properties
        private static BoardRepository _br;
        #endregion

        #region Constructor
        [ClassInitialize]
        public static void ClassInitializeBoardRepositoryTests(TestContext context)
        {
            _br = (BoardRepository)new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepository();
        }
        #endregion

        #region Test Methods
        //TODO Improve Failed Scenarios and add EditInvalid cases
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

        #endregion

        #region  Valid Assert
        [TestMethod]
        public void TestGetAllBoards()
        {
            Assert.AreEqual(3, _br.GetAll().Count());
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
            var startNBoards = _br.GetAll().Count();
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet Teste" };
            _br.Add(board);
            Assert.AreEqual(startNBoards + 1, _br.GetAll().Count());
            var addedboard = _br.FindAllBy(b => b.Name == board.Name).FirstOrDefault();
            Assert.IsNotNull(addedboard);
            _br.Delete(addedboard);
            Assert.AreEqual(0, _br.FindAllBy(b => b.Name == addedboard.Name).Count());
            Assert.AreEqual(startNBoards, _br.GetAll().Count());
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
        #endregion
        #endregion
    }
}