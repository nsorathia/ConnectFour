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

        
    }
}

