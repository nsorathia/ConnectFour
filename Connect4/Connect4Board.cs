using System;
using System.Text;
using Connect4.Interfaces;

namespace Connect4
{
    public class Connect4Board : IBoard
    {
        const int NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN = 4;

        public int Rows
        {
            get;
            private set;
        }

        public int Columns
        {
            get;
            private set;
        }

        public Token[,] Grid
        {
            get;
            private set;
        }

        //default constuctor sets grid to 6 rows and 7 columns
        public Connect4Board() : this(6, 7) { }

        public Connect4Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            var grid = new Token[rows, columns];
            Initialize(grid);
            Grid = grid;
        }

        //Initialize the Grid with empty tokens
        protected void Initialize(Token[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                    grid[i, j] = Token.Empty;
        }

            
        public override string ToString()
        {
            return "X";
        }


        public bool CheckForWin()
        {
            return false;
        }

        public bool IsUserMoveValid(int userMove)
        {
            return IsColumnFull(userMove, this.Grid);
        }

        public void SetToken(int column, Token playersToken, Token[,] grid)
        {
            if (playersToken == Token.Empty)
                throw new ArgumentException("The token to set must be either Red or Yellow");

            int columnIndex = column - 1;
            int rows = grid.GetLength(0);

            for (int i = rows; i > 0; i--)
            {
                if (grid[i - 1, columnIndex] == Token.Empty)
                {
                    grid[i - 1, columnIndex] = playersToken;
                    break;
                }
            }
        }

        public bool IsColumnFull(int columnIndex, Token[,] grid)
        {
            #region validation

            if (columnIndex < 0)
                throw new ArgumentException("The columnIndex must a positive value", "columnIndex");

            if (grid == null)
                throw new ArgumentNullException("grid");

            //Check columnIdex passed is not greater than the grid's width 
            if (columnIndex > grid.GetLength(1))
                throw new IndexOutOfRangeException("The columnIndex is greater than the width of the Board");

            #endregion

            bool columnIsFull = false;

            if (grid[0, columnIndex] != Token.Empty)
                columnIsFull = true;

            return columnIsFull;
        }

    }
}

