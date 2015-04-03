﻿using System;
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
            var startNBoards = _br.CountAsync();
            var board = new Board { Name = "Board PI Teste", Discription = "Board Programacao na Internet Teste" };
            _br.AddAsync(board);
            Assert.AreEqual(startNBoards.Result + 1, _br.CountAsync().Result);
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
            var startNBoards = _br.CountAsync();
            var boardlist = new List<Board>()
                            {
                                new Board {Name = "Board PI TestAddDeleteRange1", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange2", Discription = desc},
                                new Board {Name = "Board PI TestAddDeleteRange3", Discription = desc}
                            };
            _br.AddRangeAsync(boardlist);
            Assert.AreEqual(startNBoards.Result + 3, _br.CountAsync().Result);
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
            var eboardlist = new List<Board>
                         {
                             new Board {BoardId = 1, Name = "Board Edited1", Discription = "Board usado para editar"},
                             new Board {BoardId = 2, Name = "Board Edited2", Discription = "Board usado para editar"},
                             new Board {BoardId = 3, Name = "Board Edited3", Discription = "Board usado para editar"}
                         };
            _br.EditRangeAsync(eboardlist);
            var board = _br.GetSingleAsync(1).Result;
            Assert.AreEqual(board.BoardId, 1);
            Assert.AreEqual(board.Name, "Board Edited1");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            board = _br.GetSingleAsync(2).Result;
            Assert.AreEqual(board.BoardId, 2);
            Assert.AreEqual(board.Name, "Board Edited2");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            board = _br.GetSingleAsync(3).Result;
            Assert.AreEqual(board.BoardId, 3);
            Assert.AreEqual(board.Name, "Board Edited3");
            Assert.AreEqual(board.Discription, "Board usado para editar");
            eboardlist = new List<Board>()
                         {
                             new Board {BoardId = 1, Name = "Caetano", Discription = "Quadro do Caetano"},
                             new Board {BoardId = 2, Name = "Pedro", Discription = "Quadro do Pedro"},
                             new Board {BoardId = 3, Name = "Joao", Discription = "Quadro do Joao"}
                         };
            _br.EditRangeAsync(eboardlist);
            board = _br.GetSingleAsync(1).Result;
            Assert.AreEqual(board.BoardId, 1);
            Assert.AreEqual(board.Name, "Caetano");
            Assert.AreEqual(board.Discription, "Quadro do Caetano");
            board = _br.GetSingleAsync(2).Result;
            Assert.AreEqual(board.BoardId, 2);
            Assert.AreEqual(board.Name, "Pedro");
            Assert.AreEqual(board.Discription, "Quadro do Pedro");
            board = _br.GetSingleAsync(3).Result;
            Assert.AreEqual(board.BoardId, 3);
            Assert.AreEqual(board.Name, "Joao");
            Assert.AreEqual(board.Discription, "Quadro do Joao");
        }
        #endregion
        #endregion
    }
}