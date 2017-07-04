using System;
using System.Text;
using Connect4.Interfaces;

namespace Connect4
{
    public class Connect4Board : IBoard
    {
        const int NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN = 4;

        public Token[,] Grid
        {
            get;
            private set;
        }

        //default constuctor sets grid to 6 rows and 7 columns
        public Connect4Board() : this(6, 7) { }

        public Connect4Board(int rows, int columns)
        {
            Grid = new Token[rows, columns];
            Initialize();
        }

        //Initialize the Grid with empty tokens
        private void Initialize()
        {

        }

            
        public override string ToString()
        {
            return String.Empty;
        }


        public bool CheckForWin()
        {
            return false;
        }

        
    }
}

