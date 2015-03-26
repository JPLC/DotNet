using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrelloModel;
using TrelloModel.Repository;

namespace TrelloModelTests
{
    [TestClass]
    public class BoardRepositoryTest
    {
        private readonly BoardRepository br;

        public BoardRepositoryTest()
        {
            br = new BoardRepository();
        }

        [TestMethod]
        public void TestGetAllBoards()
        {
            //Assert.AreEqual(1,br.GetAll().Count());
        }

        [TestMethod]
        public void TestGetSingleBoard()
        {
            Assert.IsNotNull(br.GetSingle(1));
        }

        [TestMethod]
        public void TestFindBoardBy()
        {
            Assert.IsNotNull(br.FindBy(b=>b.BoardId==1));
        }

        [TestMethod]
        public void TestAddDeleteBoard()
        {
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet" };
            br.Add(board);
            var addedboard = br.FindBy(b => b.Name == board.Name).FirstOrDefault();
            Assert.IsNotNull(addedboard);
            Board del = br.GetSingle(addedboard.BoardId);
            br.Delete(del);
            Assert.IsNull(br.FindBy(b => b.Name == addedboard.Name));
        }

        [TestMethod]
        public void TestEditBoard()
        {
            var board = br.GetSingle(1);
            var eboard = new Board {BoardId = board.BoardId, Name = "Board Edited", Discription = "Board usado para editar"};
            br.Edit(eboard);
            board = br.GetSingle(1);
            Assert.Equals(board.Name,eboard.Name);
            Assert.Equals(board.Discription, eboard.Discription);
            Assert.Equals(board.BoardId, eboard.BoardId);
        }
    }
}
