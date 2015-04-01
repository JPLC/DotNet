using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
using TrelloModel.Interfaces;
using TrelloModel.Repository;
using TrelloWebAPI.Controllers;

namespace TrelloWebAPI.Tests.Controllers
{
    [TestClass]
    public class BoardControllerTest
    {
        //private static IBoardRepositoryFactory _brf;

        [TestMethod]
        public void GetAllBoardsTest()
        {
           /* var mockRepository = new Mock<IBoardRepositoryFactory>();

            var controller = new BoardController(_brf)
                             {
                                 Request = new HttpRequestMessage(),
                                 Configuration = new HttpConfiguration()
                             };

            // Act
            var response = controller.GetAllBoards();

            // Assert
            List<Board> board;
            Assert.IsTrue(response.TryGetContentValue(out board));
            //Assert.AreEqual(1, board.BoardId);*/
        }
    }
}
