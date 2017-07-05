using System;
using Connect4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4Tests
{
    [TestClass]
    public class Connect4BoardTests : Connect4Board
    {
        [TestMethod]
        public void DefaultConstuctorCreatesGridObject()
        {
            var c4Board = new Connect4Board();
            Assert.IsNotNull(c4Board.Grid);
        }

        [TestMethod]
        public void Initalize_CreatesGridWithEmptyTokens()
        {
            var random = new Random(10);
            int rows = random.Next(1, 10);
            int columns = random.Next(1, 10);

            var grid = new Token[rows, columns];
            var c4T = new Connect4BoardTests();
            c4T.Initialize(grid);

            Assert.IsTrue(rows == grid.GetLength(0));
            Assert.IsTrue(columns == grid.GetLength(1));

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Assert.IsTrue(grid[i, j] == Token.Empty);
        }

        [TestMethod]
        public void ToString_ReturnsValueNotNullOrEmpty()
        {
            var c4 = new Connect4Board();
            Assert.IsFalse(String.IsNullOrEmpty(c4.ToString()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsColumnFull_ThrowsExceptionIfColumnIndexPassedIsNegativeInteger()
        {
            var c4 = new Connect4Board();
            c4.IsColumnFull(-1, new Token[3,4]);               
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsColumnFull_ThrowsExceptionIfGridIsNull()
        {
            var c4 = new Connect4Board();
            c4.IsColumnFull(2, null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IsColumnFull_ThrowsExceptionIfColumnIndexIsGreaterThanGridWidth()
        {
            var c4 = new Connect4Board();
            c4.IsColumnFull(8, new Token[6,7]);
        }

        [TestMethod]
        public void IsColumnFull_ReturnsFalseIfColumnIsNotfull()
        {
            var c4 = new Connect4Board();
            var grid = new Token[6, 7];

            //populate and leave top row empty.
            for (int i = grid.GetLength(0) - 1; i > 0; i--)
                grid[i, 0] = Token.Red;

            Assert.IsFalse(c4.IsColumnFull(0, grid));
        }

        [TestMethod]
        public void IsColumnFull_ReturnsTrueIfColumnIsFull()
        {
            var c4 = new Connect4Board();
            var grid = new Token[6, 7];
            for (int i = 0; i < grid.GetLength(0); i++)
                grid[i, 0] = Token.Red;

            Assert.IsTrue(c4.IsColumnFull(0, grid));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetToken_ThrowsExceptionIfTokenIsEmpty()
        {
            var c4 = new Connect4Board();
            c4.SetToken(4, Token.Empty, new Token[6, 7]);
        }

        [TestMethod]
        public void SetToken_PlacesTokenInNextEmptySpace()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int rows = rnd.Next(5, 10);

            //create a grid with 1 column and random number of rows
            var grid = new Token[rows, 1];
            int rndNumOfRowsToPopulate = rnd.Next(0, rows - 1);

            //Initlaize the column with Empty tokens
            for (int i = grid.GetLength(0) - 1; i >= 0; i--)
                grid[i, 0] = Token.Empty;
            
            //Populate a random number of rows with Red starting from the botom
            for (int i = grid.GetLength(0) - 1; i >= rndNumOfRowsToPopulate; i--)
                grid[i, 0] = Token.Red;

            var c4 = new Connect4Board();

            //Test
            c4.SetToken(1, Token.Yellow, grid);

            //check that no empty tokens below the yellow token.
            int j;
            
            //find the yelow token from the top row down.
            for (j = 0; j < grid.GetLength(0); j++)
            {
                Assert.IsTrue(grid[j, 0] != Token.Red);
                if (grid[j, 0] == Token.Yellow)
                    break;
            }

            //Make sure there can be only red tokens under the yellow.
            for (int i = j + 1; i < grid.GetLength(0); i++)
                Assert.IsTrue(grid[i, 0] == Token.Red);
            
        }





    }
}
