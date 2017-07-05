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
        public void InitalizeCreatesGridWithEmptyTokens()
        {
            Random random = new Random(10);
            int rows = random.Next(1, 10);
            int columns = random.Next(1, 10);

            var grid = new Token[rows, columns];
            Connect4BoardTests c4T = new Connect4BoardTests();
            Connect4Board c42 = new Connect4Board();
            c4T.Initialize(grid);

            Assert.IsTrue(rows == grid.GetLength(0));
            Assert.IsTrue(columns == grid.GetLength(1));

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Assert.IsTrue(grid[i, j] == Token.Empty);
        }

        [TestMethod]
        public void ToStringReturnsValueNotNullOrEmpty()
        {
            Connect4Board c4 = new Connect4Board();
            Assert.IsFalse(String.IsNullOrEmpty(c4.ToString()));
        }

    }
}
