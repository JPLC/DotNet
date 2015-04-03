using System;
using System.Collections.Generic;
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
    public class BoardRepositoryAsyncTest
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
        //TODO Improve Failed Scenarios
        #region Invalid Assert
        [TestMethod]
        public void TestAddInvalidBoardNullAsync()
        {
            var board = new Board { Name = null, Discription = null };
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardEmptyAsync()
        {
            var board = new Board { Name = string.Empty, Discription = string.Empty };
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardBiggerThanMaxValueAsync()
        {
            var board = new Board { Name = new String('a', TSC.BoardNameSize + 1), Discription = new String('a', TSC.BoardDiscriptionSize + 1) };
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardInvalidCharsAsync()
        {
            var board = new Board { Name = "#$%@£@erfnerio", Discription = "#$%@£@erfnerio" };
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestAddInvalidBoardSameNameAsync()
        {
            var uniqueboard = _br.GetSingleAsync(1).Result;
            var board = new Board { Name = uniqueboard.Name, Discription = "teste repeated board" };
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardNullAsync()
        {
            var board = _br.GetSingleAsync(1).Result;
            board.Name = null; board.Discription = null;
            try
            {
                _br.EditAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardEmptyAsync()
        {
            var board = _br.GetSingleAsync(1).Result;
            board.Name = string.Empty; board.Discription = string.Empty;
            try
            {
                _br.EditAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardBiggerThanMaxValueAsync()
        {
            var board = _br.GetSingleAsync(1).Result;
            board.Name = new String('a', TSC.BoardNameSize + 1); board.Discription = new String('a', TSC.BoardDiscriptionSize + 1);
            try
            {
                _br.EditAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardInvalidCharsAsync()
        {
            var board = _br.GetSingleAsync(1).Result;
            board.Name = "#$%@£@erfnerio"; board.Discription = "#$%@£@erfnerio";
            try
            {
                _br.EditAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }

        [TestMethod]
        public void TestEditInvalidBoardSameNameAsync()
        {
            var uniqueboard = _br.GetSingleAsync(2).Result;
            var board = _br.GetSingleAsync(1).Result;
            board.Name = uniqueboard.Name; board.Discription = "teste repeated board";
            try
            {
                _br.AddAsync(board);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DbEntityValidationException);
            }
        }
        #endregion

        #region  Valid Assert
        [TestMethod]
        public void TestGetAllBoardsAsync()
        {
            var b = _br.GetAllAsync();
            var count = b.Result.Count();
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestCountAllBoardsAsync()
        {
            Assert.AreEqual(3, _br.CountAsync().Result);
        }

        [TestMethod]
        public void TestGetSingleBoardAsync()
        {
            var board = _br.GetSingleAsync(1).Result;
            Assert.IsNotNull(board);
            Assert.AreEqual(1, board.BoardId);
            Assert.AreEqual("Caetano", board.Name);
            Assert.AreEqual("Quadro do Caetano", board.Discription);
        }

        [TestMethod]
        public void TestFindBoardByAsync()
        {
            Assert.IsNotNull(_br.FindAllByAsync(b => b.BoardId == 1).Result);
            Assert.IsNotNull(_br.FindAllByAsync(b => b.Name == "Caetano").Result);
            Assert.IsNotNull(_br.FindAllByAsync(b => b.Discription == "Quadro do Caetano").Result);
        }

        [TestMethod]
        public void TestAddDeleteBoardAsync()
        {
            var startNBoards = _br.CountAsync().Result;
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet Teste" };
            _br.AddAsync(board);
            Assert.AreEqual(startNBoards + 1, _br.CountAsync().Result);
            var addedboard = _br.FindAllByAsync(b => b.Name == board.Name).Result.FirstOrDefault();
            Assert.IsNotNull(addedboard);
            _br.Delete(addedboard);
            Assert.AreEqual(0, _br.FindAllByAsync(b => b.Name == addedboard.Name).Result.Count());
            Assert.AreEqual(startNBoards, _br.CountAsync().Result);
        }

        [TestMethod]
        public void TestAddDeleteRangeBoardAsync()
        {
            const string desc = "Board PI TestAddDeleteRangeBoard";
            var startNBoards = _br.CountAsync().Result;
            var boardlist = new List<Board>()
                            {
                                new Board {Name = "Board PI TestAddDeleteRange1", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange2", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange3", Discription = desc}
                            };
            _br.AddRangeAsync(boardlist);
            Assert.AreEqual(startNBoards + 3, _br.CountAsync().Result);
            var addedboards = _br.FindAllByAsync(b => b.Discription == desc).Result;
            Assert.AreEqual(3, addedboards.Count());
            _br.DeleteRange(addedboards);
            Assert.AreEqual(0, _br.FindAllByAsync(b => b.Discription == desc).Result.Count());
            Assert.AreEqual(startNBoards, _br.CountAsync().Result);
        }

        [TestMethod]
        public void TestEditBoardAsync()
        {
            var board = _br.GetSingleAsync(2).Result;
            var eboard = new Board { BoardId = board.BoardId, Name = "Board Edited", Discription = "Board usado para editar" };
            _br.EditAsync(eboard);
            board = _br.GetSingleAsync(2).Result;
            Assert.AreEqual(board.Name, eboard.Name);
            Assert.AreEqual(board.Discription, eboard.Discription);
            Assert.AreEqual(board.BoardId, eboard.BoardId);
            eboard = new Board { BoardId = board.BoardId, Name = "Pedro", Discription = "Quadro do Pedro" };
            _br.EditAsync(eboard);
            board = _br.GetSingleAsync(2).Result;
            Assert.AreEqual(board.Name, eboard.Name);
            Assert.AreEqual(board.Discription, eboard.Discription);
            Assert.AreEqual(board.BoardId, eboard.BoardId);
        }
        
        [TestMethod]
        public void TestEditRangeBoardAsync()
        {
            var initcount = _br.CountAsync().Result;
            var eboardlist = new List<Board>
                         {
                             new Board {Name = "Board EditedAsync1", Discription = "Board usado para EditRangeBoardAsync"},
                             new Board {Name = "Board EditedAsync2", Discription = "Board usado para EditRangeBoardAsync"},
                             new Board {Name = "Board EditedAsync3", Discription = "Board usado para EditRangeBoardAsync"}
                         };
            _br.AddRangeAsync(eboardlist);
            Assert.AreEqual(initcount + eboardlist.Count, _br.CountAsync().Result);
            var board1 = _br.FindByAsync(b=>b.Name=="Board EditedAsync1").Result;
            Assert.AreEqual(board1.Name, "Board EditedAsync1");
            Assert.AreEqual(board1.Discription, "Board usado para EditRangeBoardAsync");
            var board2 = _br.FindByAsync(b=>b.Name=="Board EditedAsync2").Result;
            Assert.AreEqual(board2.Name, "Board EditedAsync2");
            Assert.AreEqual(board2.Discription, "Board usado para EditRangeBoardAsync");
            var board3 = _br.FindByAsync(b=>b.Name=="Board EditedAsync3").Result;
            Assert.AreEqual(board3.Name, "Board EditedAsync3");
            Assert.AreEqual(board3.Discription, "Board usado para EditRangeBoardAsync");
            eboardlist = new List<Board>
                         {
                             new Board {BoardId = board1.BoardId, Name = "Board EditedAsync11", Discription = "Board usado para EditRangeBoardAsync1"},
                             new Board {BoardId = board2.BoardId, Name = "Board EditedAsync22", Discription = "Board usado para EditRangeBoardAsync2"},
                             new Board {BoardId = board3.BoardId, Name = "Board EditedAsync33", Discription = "Board usado para EditRangeBoardAsync3"}
                         };
            _br.EditRangeAsync(eboardlist);
            board1 = _br.GetSingleAsync(board1.BoardId).Result;
            Assert.AreEqual(board1.Name, "Board EditedAsync11");
            Assert.AreEqual(board1.Discription, "Board usado para EditRangeBoardAsync1");
            board2 = _br.GetSingleAsync(board2.BoardId).Result;
            Assert.AreEqual(board2.Name, "Board EditedAsync22");
            Assert.AreEqual(board2.Discription, "Board usado para EditRangeBoardAsync2");
            board3 = _br.GetSingleAsync(board3.BoardId).Result;
            Assert.AreEqual(board3.Name, "Board EditedAsync33");
            Assert.AreEqual(board3.Discription, "Board usado para EditRangeBoardAsync3");
            _br.DeleteRangeAsync(eboardlist);
            Assert.AreEqual(initcount ,_br.CountAsync().Result);
        }
        
        #endregion
        #endregion
    }
}