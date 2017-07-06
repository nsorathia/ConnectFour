using System;
using Connect4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4Tests
{
    /// <summary>
    /// Test class inherits Connect4Board to access protected methods.
    /// </summary>
    [TestClass]
    public class Connect4BoardTests : Connect4Board
    {
        public Connect4BoardTests() : base()
        {
        }

        public Connect4BoardTests(int rows, int columns) : base(rows, columns)
        {
        }

        [TestMethod]
        public void ConstuctorCreatesGridObjectAndInitializesWithEmptyTokens()
        {
            var board1 = new Connect4Board();
            Assert.IsNotNull(board1.Grid);

            for (int i = 0; i < board1.Rows; i++)
                for (int j = 0; j < board1.Columns; j++)
                    Assert.IsTrue(board1.Grid[i, j] == Token.Empty);
        }

        [TestMethod]
        public void ToString_ReturnsValueNotNullOrEmpty()
        {
            var c4 = new Connect4Board();
            Assert.IsFalse(String.IsNullOrEmpty(c4.ToString()));
        }

        [TestMethod]
        public void IsColumnFull_ReturnsFalseIfColumnIsNotfull()
        {
            var c4 = new Connect4BoardTests(6,7);
            
            //populate and leave top row empty.
            for (int i = c4.Rows - 1; i > 0; i--)
                c4.Grid[i, 0] = Token.Red;

            Assert.IsFalse(c4.IsColumnFull(0));
        }

        [TestMethod]
        public void IsColumnFull_ReturnsTrueIfColumnIsFull()
        {
            var c4 = new Connect4BoardTests();
           
            for (int i = 0; i < c4.Rows; i++)
                c4.Grid[i, 0] = Token.Red;

            Assert.IsTrue(c4.IsColumnFull(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUserMove_ThrowsExceptionIfTokenIsEmpty()
        {
            var c4 = new Connect4Board();
            c4.SetUserMove(4, Token.Empty);
        }

        [TestMethod]
        public void SetUserMove_PlacesTokenInNextEmptySpace()
        {
            Connect4BoardTests board;
            int rndNumOfRowsToPopulate;
            CreateBoardWithOneColumnWithRandomNumberRedTokens(out board, out rndNumOfRowsToPopulate);

            //Test
            board.SetUserMove(1, Token.Yellow);

            int j;
            
            //find the yelow token from the top row down.
            for (j = 0; j < board.Rows; j++)
            {
                Assert.IsTrue(board.Grid[j, 0] != Token.Red);
                if (board.Grid[j, 0] == Token.Yellow)
                    break;
            }

            //Make sure there can be only red tokens under the yellow.
            for (int i = j + 1; i < board.Rows; i++)
                Assert.IsTrue(board.Grid[i, 0] == Token.Red);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CheckForWin_DoesNotAllowNegativeLastMoveParameter()
        {
            var c4 = new Connect4Board();
            c4.CheckForWin(-1);
        }



        [TestMethod]
        public void SetLeftUpperCoordinateForDownDiagonalWin_ReturnsProperLeftUpperCoordinate()
        {
            //input
            int rowindex = 2, columnIndex = 3;
            int expectedOutRowIndex = 0, expectedOutColumnIndex = 1;
            
            //output
            int outRowIndex, outColumnIndex;
            this.SetLeftUpperCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);

            //Test1
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 4; columnIndex = 1;
            expectedOutRowIndex = 3; expectedOutColumnIndex = 0;
            this.SetLeftUpperCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 0; columnIndex = 3;
            expectedOutRowIndex = 0; expectedOutColumnIndex = 3;
            this.SetLeftUpperCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);
            
        }

        [TestMethod]
        public void SetRightLowerCoordinateForDownDiagonalWin_ReturnsProperRightLowerCoordinate()
        {
            //input
            int rowindex = 2, columnIndex = 3;
            int expectedOutRowIndex = 5, expectedOutColumnIndex = 6;

            //output
            int outRowIndex, outColumnIndex;
            this.SetRightLowerCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);

            //Test1
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 4; columnIndex = 1;
            expectedOutRowIndex = 5; expectedOutColumnIndex = 2;
            this.SetRightLowerCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test3
            rowindex = 0; columnIndex = 3;
            expectedOutRowIndex = 3; expectedOutColumnIndex = 6;
            this.SetRightLowerCoordinateForDownDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

        }
        
        [TestMethod]
        public void SetLeftLowerCoordinateForUpDiagonalWin_ReturnsProperRightLowerCoordinate()
        {
            //input
            int rowindex = 2, columnIndex = 3;
            int expectedOutRowIndex = 5, expectedOutColumnIndex = 0;

            //output
            int outRowIndex, outColumnIndex;
            this.SetLeftLowerCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);

            //Test1
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 4; columnIndex = 1;
            expectedOutRowIndex = 5; expectedOutColumnIndex = 0;
            this.SetLeftLowerCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 0; columnIndex = 3;
            expectedOutRowIndex = 3; expectedOutColumnIndex = 0;
            this.SetLeftLowerCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);
        }

        [TestMethod]
        public void SetRightUpperCoordinateForUpDiagonalWin_ReturnsProperRightLowerCoordinate()
        {
            //input
            int rowindex = 2, columnIndex = 3;
            int expectedOutRowIndex = 0, expectedOutColumnIndex = 5;

            //output
            int outRowIndex, outColumnIndex;
            this.SetRightUpperCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);

            //Test1
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 4; columnIndex = 1;
            expectedOutRowIndex = 1; expectedOutColumnIndex = 4;
            this.SetRightUpperCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);

            //Test2
            rowindex = 0; columnIndex = 3;
            expectedOutRowIndex = 0; expectedOutColumnIndex = 3;
            this.SetRightUpperCoordinateForUpDiagonalTest(rowindex, columnIndex, out outRowIndex, out outColumnIndex);
            Assert.IsTrue(outRowIndex == expectedOutRowIndex && outColumnIndex == expectedOutColumnIndex);
        }

        [TestMethod]
        public void FindRowIndexForLastDroppedToken_ReturnsCorrectRowIndex()
        {
            Connect4BoardTests board;
            int rndNumOfRowsToPopulate;
            CreateBoardWithOneColumnWithRandomNumberRedTokens(out board, out rndNumOfRowsToPopulate);
            
            int rowIndex = board.FindRowIndexForLastDroppedToken(0);
            Assert.IsTrue(rowIndex == board.Rows - rndNumOfRowsToPopulate);
        }

        [TestMethod]
        public void IsHorizontalWin_ConfirmsIfrowContains4ConsecutiveTokens()
        {
            //Create a Random sized grid and place 4 consecutive red tokens in a random horizontal row 
            int maxRows = 15, maxColumns = 20;

            var board = GetRandomSizedBoard(maxRows, maxColumns);
            int rows = board.Rows;
            int columns = board.Columns;

            var rand = new Random(DateTime.Now.Millisecond);
            int randomRow = rand.Next(5, rows - 5);
            int randomColumn = rand.Next(5, columns - 5);

            //Test1: Set 4 consecutive red in row
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN; i++)
                board.Grid[randomRow, randomColumn + i] = Token.Red;

            Assert.IsTrue(board.IsHorizontalWin(randomRow, randomColumn));

            //Test2: Set 2 consecutive red in previous
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - 1, randomColumn + i] = Token.Red;

            Assert.IsFalse(board.IsHorizontalWin(randomRow - 1, randomColumn));
            
        }

        [TestMethod]
        public void IsVerticalWin_ConfirmsIfColumnContains4ConsecutiveTokens()
        {
            //Create a Random sized grid and place 4 consecutive red tokens in a random horizontal row 
            int maxRows = 15, maxColumns = 20;

            var board = GetRandomSizedBoard(maxRows, maxColumns);
            int rows = board.Rows;
            int columns = board.Columns;

            var rand = new Random(DateTime.Now.Millisecond);
            int randomRow = rand.Next(5, rows - 5);
            int randomColumn = rand.Next(5, columns - 5);

            //Test1: Set 4 consecutive red in column
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN; i++)
                board.Grid[randomRow + i, randomColumn] = Token.Red;

            Assert.IsTrue(board.IsVerticalWin(randomRow, randomColumn));

            //Test2:  Set 3 consecutive Red in previous column.
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 1; i++)
                board.Grid[randomRow + i, randomColumn - 1] = Token.Red;

            Assert.IsFalse(board.IsVerticalWin(randomRow, randomColumn -1));

        }

        [TestMethod] 
        public void IsUpDiagonalWin_ConfirmsIfDiagnalContains4ConsecutiveTokens()
        {
            //Create a Random sized grid and place 4 consecutive red tokens in a random horizontal row 
            int maxRows = 25, maxColumns = 25;

            var board = GetRandomSizedBoard(maxRows, maxColumns);
            int rows = board.Rows;
            int columns = board.Columns;

            var rand = new Random(DateTime.Now.Millisecond);
            int randomRow = rand.Next(10, rows - 5);
            int randomColumn = rand.Next(10, columns - 5);

            //Test1: Set 4 consecutive red in up-diagnal pattern
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 1; i++)
                board.Grid[randomRow + i, randomColumn - i] = Token.Red;

            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 1; i++)
                board.Grid[randomRow - i, randomColumn + i] = Token.Red;

            Assert.IsTrue(board.IsUpDiagonalWin(randomRow, randomColumn));

            //Test2:  Set 3 consecutive Red in previous column.
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - 5 + i, randomColumn - i] = Token.Red;

            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - 5 - i, randomColumn + i] = Token.Red;

            Assert.IsFalse(board.IsUpDiagonalWin(randomRow - 5, randomColumn));

        }

        [TestMethod]
        public void IsDownDiagonalWin_ConfirmsIfDiagnalContains4ConsecutiveTokens()
        {
            //Create a Random sized grid and place 4 consecutive red tokens in a random horizontal row 
            int maxRows = 25, maxColumns = 25;

            var board = GetRandomSizedBoard(maxRows, maxColumns);
            int rows = board.Rows;
            int columns = board.Columns;

            var rand = new Random(DateTime.Now.Millisecond);
            int randomRow = rand.Next(10, rows - 5);
            int randomColumn = rand.Next(10, columns - 5);

            //Test1: Set 4 consecutive red in up-diagnal pattern
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 1; i++)
                board.Grid[randomRow + i, randomColumn + i] = Token.Red;

            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - i, randomColumn - i] = Token.Red;

            Assert.IsTrue(board.IsDownDiagonalWin(randomRow, randomColumn));

            //Test2:  Set 3 consecutive Red in previous column.
            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - 5 + i, randomColumn + i] = Token.Red;

            for (int i = 0; i < NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN - 2; i++)
                board.Grid[randomRow - 5 - i, randomColumn - i] = Token.Red;

            Assert.IsFalse(board.IsDownDiagonalWin(randomRow - 5, randomColumn));

        }




        
        private void CreateBoardWithOneColumnWithRandomNumberRedTokens(out Connect4BoardTests board, out int rndNumOfRowsToPopulate)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int randRows = rnd.Next(5, 10);
            
            //create a grid with 1 column and random number of rows
            board = new Connect4BoardTests(randRows, 1);
            rndNumOfRowsToPopulate = rnd.Next(1, randRows - 1);

            //Populate a random number of rows with Red starting from the botom
            for (int i = board.Rows - 1; i >= board.Rows - rndNumOfRowsToPopulate; i--)
                board.Grid[i, 0] = Token.Red;
        }

        /// <summary>
        /// Helper method to create random sized board initialized with empty tokens.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private Connect4BoardTests GetRandomSizedBoard(int min, int max)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var board = new Connect4BoardTests(rnd.Next(min, max), rnd.Next(min, max));

            for (int i = 0; i < board.Rows; i++)
                for (int j = 0; j < board.Columns; j++)
                    board.Grid[i, j] = Token.Empty;

            return board;
        }


    }
}
