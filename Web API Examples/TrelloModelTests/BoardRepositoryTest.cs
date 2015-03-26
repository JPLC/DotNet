using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrelloModel;
using TrelloModel.Factories;
using TrelloModel.Repository;

namespace TrelloModelTests
{
    [TestClass]
    public class BoardRepositoryTest
    {
        private readonly BoardRepository _br;

        public BoardRepositoryTest()
        {
           _br = (BoardRepository) new RepositoryConcreteFactory().GetBoardFactory().GetBoardRepository();
        }

        [TestMethod]
        public void TestGetAllBoards()
        {
            //Assert.AreEqual(1,br.GetAll().Count());
        }

        [TestMethod]
        public void TestGetSingleBoard()
        {
            Assert.IsNotNull(_br.GetSingle(1));
        }

        [TestMethod]
        public void TestFindBoardBy()
        {
            Assert.IsNotNull(_br.FindBy(b=>b.BoardId==1));
        }

        [TestMethod]
        public void TestAddDeleteBoard()
        {
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet" };
            _br.Add(board);
            var addedboard = _br.FindBy(b => b.Name == board.Name).FirstOrDefault();
            Assert.IsNotNull(addedboard);
            Board del = _br.GetSingle(addedboard.BoardId);
            _br.Delete(del);
            Assert.IsNull(_br.FindBy(b => b.Name == addedboard.Name));
        }

        [TestMethod]
        public void TestEditBoard()
        {
            var board = _br.GetSingle(1);
            var eboard = new Board {BoardId = board.BoardId, Name = "Board Edited", Discription = "Board usado para editar"};
            _br.Edit(eboard);
            board = _br.GetSingle(1);
            Assert.Equals(board.Name,eboard.Name);
            Assert.Equals(board.Discription, eboard.Discription);
            Assert.Equals(board.BoardId, eboard.BoardId);
        }
    }
}
