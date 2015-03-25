using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrelloModel;
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
            //Assert.AreEqual(1,br.GetAll().Count());
            br.Add(new Board{ Name="",Discription = ""});
            //var x = br.ExecuteSP("ProcedureTest");
        }
    }
}
