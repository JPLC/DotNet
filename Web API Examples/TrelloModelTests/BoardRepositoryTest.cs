using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;

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
        #region Invalid Assert
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
            Assert.IsNotNull(_br.GetSingle(1));
        }

        [TestMethod]
        public void TestFindBoardBy()
        {
            Assert.IsNotNull(_br.FindBy(b => b.BoardId == 1));
        }

        [TestMethod]
        public void TestAddDeleteBoard()
        {
            var startNBoards = _br.GetAll().Count();
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet Teste" };
            _br.Add(board);
            Assert.AreEqual(startNBoards + 1, _br.GetAll().Count());
            var addedboard = _br.FindBy(b => b.Name == board.Name).FirstOrDefault();
            Assert.IsNotNull(addedboard);
            _br.Delete(addedboard);
            Assert.AreEqual(0, _br.FindBy(b => b.Name == addedboard.Name).Count());
            Assert.AreEqual(startNBoards, _br.GetAll().Count());
        }

        [TestMethod]
        public void TestEditBoard()
        {
            var board = _br.GetSingle(1);
            var eboard = new Board { BoardId = board.BoardId, Name = "Board Edited", Discription = "Board usado para editar" };
            _br.Edit(eboard);
            board = _br.GetSingle(1);
            Assert.AreEqual(board.Name, eboard.Name);
            Assert.AreEqual(board.Discription, eboard.Discription);
            Assert.AreEqual(board.BoardId, eboard.BoardId);
        }
        #endregion
        #endregion
    }
}