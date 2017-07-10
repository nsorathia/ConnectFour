using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Connect4;
using Connect4.Interfaces;
using Connect4.Algorithm;
using Connect4.Board;
using System.Collections.Generic;

namespace Connect4Tests
{
    [TestClass]
    public class MinMaxAlgorithmTests : MinMaxAlgorithm
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GraphVersions_ThrowsExceptionifBoardVersionsIsNull()
        {
            var algo = new MinMaxAlgorithmTests();
            algo.GraphVersions(null, Token.Red, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GraphVersions_ThrowsExceptionifTokenIsEmpty()
        {
            Mock<IBoard> mockBoard = new Mock<IBoard>();

            var algo = new MinMaxAlgorithmTests();
            algo.GraphVersions(new BoardVersion(mockBoard.Object), Token.Empty, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GraphVersions_ThrowsExceptionifLevelIsLessthanZero()
        {
            Mock<IBoard> mockBoard = new Mock<IBoard>();

            var algo = new MinMaxAlgorithmTests();
            algo.GraphVersions(new BoardVersion(mockBoard.Object), Token.Yellow, -1);
        }


        [TestMethod]
        public void GraphVersions_CreatesAGraph_nlevels_deep()
        {
            var board = new Connect4Board();
            var boardVersion = new BoardVersion(board);

            var rand = new Random(DateTime.Now.Millisecond);
            int recursionlevel = rand.Next(1, 6);

            var algo = new MinMaxAlgorithmTests();
            algo.GraphVersions(boardVersion, Token.Yellow, recursionlevel);

            BoardVersion testBoardVersion = boardVersion;
            for (int i = 1; i <= recursionlevel; i++)
            {
                testBoardVersion = testBoardVersion.OppMoveVersions[0];
            }

            Assert.IsTrue(testBoardVersion.OppMoveVersions.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateScore_ThrowsExceptionifBoardVersionsIsNull()
        {
            var algo = new MinMaxAlgorithmTests();
            algo.CalculateScore(null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateScore_ThrowsExceptionifLevelIsLessthanZero()
        {
            Mock<IBoard> mockBoard = new Mock<IBoard>();

            var algo = new MinMaxAlgorithmTests();
            algo.CalculateScore(new BoardVersion(mockBoard.Object), -1);
        }

        [TestMethod]
        public void CalculateScore_ReturnsMaxScoreifCurrentBoardIsWin()
        {
            var c4Board = new Connect4Board();
            c4Board.SetUserMove(1, Token.Red);
            c4Board.SetUserMove(1, Token.Red);
            c4Board.SetUserMove(1, Token.Red);
            c4Board.SetUserMove(1, Token.Red);

            var random = new Random(DateTime.Now.Millisecond);
            int level = random.Next(1, 6);

            var algo = new MinMaxAlgorithmTests();
            
            int expectedScore = (int)Math.Pow(10, level);
            int score = algo.CalculateScore(new BoardVersion(c4Board), level);

            Assert.IsTrue(expectedScore == score);
        }


        [TestMethod]
        public void CalculateScore_ReturnsMinScoreifCurrentBoardIsWin()
        {
            var random = new Random(DateTime.Now.Millisecond);
            int level = random.Next(1, 6);

            var c4Board = new Connect4Board();

            //set red to win on next play
            c4Board.SetUserMove(3, Token.Red);
            c4Board.SetUserMove(3, Token.Red);
            c4Board.SetUserMove(3, Token.Red);

            //set a yellow tokens on the board.
            c4Board.SetUserMove(7, Token.Yellow);
            c4Board.SetUserMove(7, Token.Yellow);

            //test move
            c4Board.SetUserMove(7, Token.Yellow);

            var boardVersion = new BoardVersion(c4Board);
            GraphVersions(boardVersion, Token.Yellow, level);            

            int expectedScore = -(int)Math.Pow(10, level);

            //calculate the score of the above test move
            var algo = new MinMaxAlgorithmTests();
            int score = algo.CalculateScore(boardVersion, level);

            //the score is expected to be the negative value of the maximum score 
            //becasue yellow can win and fails to bock a red win.
            Assert.IsTrue(expectedScore == score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBoardVersion_ThrowsExceptionifBoardIsNull()
        {
            var algo = new MinMaxAlgorithmTests();
            algo.CreateBoardVersion(null, 1, Token.Red);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBoardVersion_ThrowsExceptionifMoveIsNegativeValue()
        {
            Mock<IBoard> mockBoard = new Mock<IBoard>();

            var algo = new MinMaxAlgorithmTests();
            algo.CreateBoardVersion(mockBoard.Object, -1, Token.Red);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBoardVersion_ThrowsExceptionifTokenIsEmpty()
        {
            Mock<IBoard> mockBoard = new Mock<IBoard>();

            var algo = new MinMaxAlgorithmTests();
            algo.CreateBoardVersion(mockBoard.Object, 2, Token.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesBoardVersionsContainAWin_ThrowsExceptionifListIsNull()
        {
            var algo = new MinMaxAlgorithmTests();

            algo.DoesBoardVersionsContainAWin(null);
        }

        [TestMethod]
        public void DoesBoardVersionsContainAWin_ReturnsTrueIfAtleastOneVersionIsWin()
        {
            var board = new Connect4Board();
            board.SetUserMove(1, Token.Red);
            board.SetUserMove(2, Token.Red);
            board.SetUserMove(3, Token.Red);

            var algo = new MinMaxAlgorithmTests();


            int columnIndex = 3;

            //winner
            var v1 = algo.CreateBoardVersion(board.Clone(), columnIndex, Token.Red);

            //other versions
            var v2 = algo.CreateBoardVersion(board.Clone(), columnIndex++, Token.Red);
            var v3 = algo.CreateBoardVersion(board.Clone(), columnIndex++, Token.Red);
            var v4 = algo.CreateBoardVersion(board.Clone(), columnIndex++, Token.Red);
            
            var versions = new List<BoardVersion> { v1, v2, v3, v4 };

            Assert.IsTrue(algo.DoesBoardVersionsContainAWin(versions));
        }

        [TestMethod]
        public void DoesBoardVersionsContainAWin_ReturnsFalseIfNoVersionIsAWin()
        {
            var board = new Connect4Board();
            int columnNumber = 1;
            board.SetUserMove(columnNumber, Token.Red);
            board.SetUserMove(columnNumber++, Token.Red);
            board.SetUserMove(columnNumber++, Token.Red);

            var algo = new MinMaxAlgorithmTests();

            //other versions
            int columnIndex = 4;
            var v1 = algo.CreateBoardVersion(board.Clone(), columnIndex, Token.Red);
            var v2 = algo.CreateBoardVersion(board.Clone(), columnIndex++, Token.Red);
            var v3 = algo.CreateBoardVersion(board.Clone(), columnIndex++, Token.Red);
            
            var versions = new List<BoardVersion> { v1, v2, v3};

            Assert.IsFalse(algo.DoesBoardVersionsContainAWin(versions));
        }
        
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetIndexOfBestScoredMove_ThrowsExceptionVersionsisNull()
        {
            var algo = new MinMaxAlgorithmTests();
            algo.GetIndexOfBestScoredMove(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexOfBestScoredMove_ThrowsExceptionIfVersionsIsEmpty()
        {
            var algo = new MinMaxAlgorithmTests();

            var versions = new BoardVersion[0];
            algo.GetIndexOfBestScoredMove(versions);
        }
        
        [TestMethod]
        public void GetIndexOfBestScoredMove_ReturnsIndexOfVersionWithHighestPts()
        {
            var algo = new MinMaxAlgorithmTests();

            var versions = new BoardVersion[5];
            versions[0] = new BoardVersion(new Connect4Board());
            versions[1] = new BoardVersion(new Connect4Board());
            versions[2] = new BoardVersion(new Connect4Board());
            versions[3] = new BoardVersion(new Connect4Board());
            versions[4] = new BoardVersion(new Connect4Board());

            versions[0].Points = 4;
            versions[1].Points = 9;
            versions[2].Points = 7;
            versions[3].Points = 9;
            versions[4].Points = 8;

            int index = algo.GetIndexOfBestScoredMove(versions);

            Assert.IsTrue(index == 1 || index == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateBestMove_ThrowsExceptionIfBoardIsNull()
        {
            var algo = new MinMaxAlgorithm();
            algo.CalculateBestMove(null, Token.Red);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateBestMove_ThrowsExceptionIfTokenIsEmpty()
        {
            var algo = new MinMaxAlgorithm();
            algo.CalculateBestMove(new Connect4Board(), Token.Empty); ;
        }
    }
}
