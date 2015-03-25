using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel.Repository;

namespace TrelloModelTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BoardRepository br = new BoardRepository();
            Assert.AreSame(5,br.GetAll().Count());
        }
    }
}
