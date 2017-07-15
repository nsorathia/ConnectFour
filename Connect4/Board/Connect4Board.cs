using System;
using System.Text;
using Connect4.Interfaces;
using System.Collections.Generic;

namespace Connect4.Board
{
    public class Connect4Board : IBoard
    {
        public const int NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN = 4;

        #region Constr

        //default constuctor sets grid to 6 rows and 7 columns
        public Connect4Board() : this(6, 7) { }

        public Connect4Board(int rows, int columns)
        {
            Grid = new Token[rows, columns];
            Rows = rows;
            Columns = columns;

            Initialize();            
        }

        public Connect4Board(Token[,] grid)
        {
            this.Rows = grid.GetLength(0);
            this.Columns = grid.GetLength(1);
            this.Grid = new Token[this.Rows, this.Columns];

            for (int i = 0; i < this.Rows; i++)
                for (int j = 0; j < this.Columns; j++)
                    this.Grid[i, j] = grid[i, j];
        }

        #endregion

        #region Implemented IBoard methods and properties

        /// <summary>
        /// Implements IBoard.Grid
        /// </summary>
        public Token[,] Grid
        {
            get;
            set;
        }

        /// <summary>
        /// Implements IBoard.Rows
        /// </summary>
        public int Rows
        {
            get;
            set;
        }

        /// <summary>
        /// Implements IBoard.Columns
        /// </summary>
        public int Columns
        {
            get;
            set;
        }

        /// <summary>
        /// ColumnIndex of the Last move
        /// </summary>
        public int LastMove
        {
            get;
            set;
        }

        /// <summary>
        /// Implements IBoard.CheckforWin: Test the board for a win given the column number of the last token played   
        /// </summary>
        /// <param name="lastMove"></param>
        /// <returns>bool</returns>
        public bool CheckForWin(int lastMove)
        {
            if (lastMove < 1)
                throw new ArgumentOutOfRangeException("lastUserMove", "the lastUserMove Paramater must be > 0");

            bool win = false;

            int columnIndex = lastMove - 1;
            int rowIndex = FindRowIndexForLastDroppedToken(columnIndex);
          
            if (IsHorizontalWin(rowIndex, columnIndex)
                || IsVerticalWin(rowIndex, columnIndex)
                || IsUpDiagonalWin(rowIndex, columnIndex)
                || IsDownDiagonalWin(rowIndex, columnIndex))
                win = true;

            return win;
        }

        /// <summary>
        /// Implements IBoard.IsUserMoveValid. Tests whether User's chosen move is valid
        /// </summary>
        /// <param name="userMove"></param>
        /// <returns></returns>
        public bool IsUserMoveValid(int userMove)
        {
            return IsColumnIndexWithinBounds(userMove) && !IsColumnFull(userMove);
        }

        /// <summary>
        /// Implements SetUserMove: Updates the boards grid with the 
        /// player's token given the column number
        /// </summary>
        /// <param name="move"></param>
        /// <param name="token"></param>
        public void SetUserMove(int move, Token token)
        {
            if (token == Token.Empty)
                throw new ArgumentException("The token to set must be either Red or Yellow");

            int columnIndex = move - 1;

            for (int i = this.Rows; i > 0; i--)
            {
                int rowIndex = i - 1;
                //sets the token in the lowest empty spot for the column
                //and set the LastMove property 
                if (this.Grid[rowIndex, columnIndex] == Token.Empty)
                {
                    this.Grid[rowIndex, columnIndex] = token;
                    this.LastMove = columnIndex;
                    break;
                }
            }
        }

        //TODO: ADD TEST
        /// <summary>
        /// Implements IBoad.Clone(): Create a clone for the board.
        /// </summary>
        /// <returns></returns>
        public IBoard Clone()
        {
            return new Connect4Board(this.Grid);
        }

        //TODO:  ADD TEST
        /// <summary>
        /// Implements IBoard.GetAvailableMoves(): Returns a list of columns which are not full
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public IList<int> GetAvailableMoves()
        {
            var list = new List<int>();

            for (int i = 0; i < this.Columns; i++)
            {
                if (!IsColumnFull(i))
                    list.Add(i);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// String Representation of the Connect4 Board. [X: Red], [O: Yellow], [#: Empty]
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>string</returns>
        public override string ToString()
        {
            var sBuilder = new StringBuilder();

            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    string marker = " # ";

                    var gridToken = this.Grid[i, j];

                    if (gridToken == Token.Red)
                        marker = " X ";

                    else if (gridToken == Token.Yellow)
                        marker = " O ";

                    sBuilder.Append(marker);
                }

                sBuilder.AppendLine();
            }

            return sBuilder.ToString();
        }

        #region Protected Methods 
        //marked protected so they can be Accessible for testing via inherited class (Connect4BoardTests)

        /// <summary>
        /// Initializes the Grid with empty tokens
        /// </summary>
        protected void Initialize()
        {
            for (int i = 0; i < this.Rows; i++)
                for (int j = 0; j < this.Columns; j++)
                    this.Grid[i, j] = Token.Empty;
        }

        /// <summary>
        /// Checks if the columnIndex is within the bounds of the board's grid.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsColumnIndexWithinBounds(int columnIndex)
        {
            int uBound = this.Grid.GetUpperBound(1);
            if (columnIndex >= 0 && columnIndex <= uBound)
                return true;

            return false;
        }


        /// <summary>
        /// Determines if the specified Column is Full
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsColumnFull(int columnIndex)
        {
            bool columnIsFull = false;

            if (this.Grid[0, columnIndex] != Token.Empty)
                columnIsFull = true;

            return columnIsFull;
        }
        
        /// <summary>
        /// Test for Horizontal win given the coordinates of the last dropped token 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsHorizontalWin(int rowIndex, int columnIndex)
        {
            var token = this.Grid[rowIndex, columnIndex];

            //set search bounds +- 3 from last droped token
            int leftBound = Math.Max(0, columnIndex - 3);
            int rightBound = Math.Min(columnIndex + 3, this.Columns - 1);

            int count = 0;

            for (int i = leftBound; i <= rightBound; i++)
            {
                if (this.Grid[rowIndex, i] == token)
                    count++;
                else
                    count = 0;

                if (count == NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN)
                    return true;
            }

            return false;
        }
        
        /// <summary>
        /// Test for Vertical win given the coordinates of the last dropped token 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsVerticalWin(int rowIndex, int columnIndex)
        {
            var token = this.Grid[rowIndex, columnIndex];

            //set search bounds +- 3 given last dropped token.
            int upperBound = Math.Min(rowIndex + 3, this.Rows - 1);
            int lowerBound = rowIndex; //last dropped token is lowerBound 

            int count = 0;

            for (int i = lowerBound; i <= upperBound; i++)
            {
                if (this.Grid[i, columnIndex] == token)
                    count++;
                else
                    count = 0;

                if (count == NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Test for UpDiaganol Win given last dropped token
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsUpDiagonalWin(int rowIndex, int columnIndex)
        {
            var token = this.Grid[rowIndex, columnIndex];
            
            //sets the low-left and high-right index bounds
            int leftLowerRowIndex, leftLowerColumnIndex;
            SetLeftLowerCoordinateForUpDiagonalTest(rowIndex, columnIndex, out leftLowerRowIndex, out leftLowerColumnIndex);

            int rightUpperRowIndex, rightUpperColumnIndex;
            SetRightUpperCoordinateForUpDiagonalTest(rowIndex, columnIndex, out rightUpperRowIndex, out rightUpperColumnIndex);

            //test if there are less than 4 tokens to check
            if (rightUpperColumnIndex - leftLowerColumnIndex < 3)
                return false;

            int count = 0;

            for (int i = leftLowerRowIndex, j = leftLowerColumnIndex; i >= rightUpperRowIndex && j <= rightUpperColumnIndex; i--, j++)
            {
                if (this.Grid[i, j] == token)
                {
                    count++;

                    if (count == NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN)
                        return true;
                }
                else
                    count = 0;
            }

            return false;
        }

        /// <summary>
        /// Test for DownDiaganol Win given last dropped token 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        protected bool IsDownDiagonalWin(int rowIndex, int columnIndex)
        {
            var token = this.Grid[rowIndex, columnIndex];

            //set high-left and low-right bounds
            int leftUpperRowIndex, leftUpperColumnIndex;
            SetLeftUpperCoordinateForDownDiagonalTest(rowIndex, columnIndex, out leftUpperRowIndex, out leftUpperColumnIndex);

            int rightLowerRowIndex, rightLowerColumnIndex;
            SetRightLowerCoordinateForDownDiagonalTest(rowIndex, columnIndex, out rightLowerRowIndex, out rightLowerColumnIndex);

            //no need to seach further if there are less than 4 tokens to check
            if (rightLowerRowIndex - leftUpperRowIndex < 3)
                return false;

            int count = 0;

            for (int i = leftUpperRowIndex, j = leftUpperColumnIndex; i <= rightLowerRowIndex && j <= rightLowerColumnIndex; i++, j++)
            {
                if (this.Grid[i, j] == token)
                {
                    count++;

                    if (count == NUM_OF_CONSECUTIVE_TOKENS_FOR_WIN)
                        return true;
                }
                else
                    count = 0;
            }

            return false;
        }

        /// <summary>
        /// Returns the rowIndex for the last Dropped token given the last move's column indexs, 
        /// </summary>
        /// <param name="lastMoveColumnIndex"></param>
        /// <returns></returns>
        protected int FindRowIndexForLastDroppedToken(int lastMoveColumnIndex)
        {
            if (lastMoveColumnIndex < 0 || lastMoveColumnIndex > this.Columns - 1)
                throw new ArgumentOutOfRangeException("lastMoveColumnIndex", "The lastMoveColumnIndex must be within the bounds of the grid");
            
            int i;
            for (i = 0; i < this.Rows; i++)
            {
                //start from the top and find the next non empty row 
                var token = this.Grid[i, lastMoveColumnIndex];
                if (token != Token.Empty)
                    break;
            }

            return i;
        }

        /// <summary>
        /// Determines the left upper bound for a down diagnal test  given coordinates of the last dropped token
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="leftUpperRowIndex"></param>
        /// <param name="leftUpperColumnIndex"></param>
        protected void SetLeftUpperCoordinateForDownDiagonalTest(int rowIndex, int columnIndex, out int leftUpperRowIndex, out int leftUpperColumnIndex)
        {
            //initialize
            leftUpperRowIndex = rowIndex;
            leftUpperColumnIndex = columnIndex;

            if (rowIndex == 0 || columnIndex == 0)
                return;

            else if (rowIndex == 1 || columnIndex == 1)
            {
                leftUpperRowIndex = rowIndex - 1;
                leftUpperColumnIndex = columnIndex - 1;
                return;
            }

            else if (rowIndex == 2 || columnIndex == 2)
            {
                leftUpperRowIndex = rowIndex - 2;
                leftUpperColumnIndex = columnIndex - 2;
                return;
            }

            else if (rowIndex >= 3 || columnIndex >= 3)
            {
                leftUpperRowIndex = rowIndex - 3;
                leftUpperColumnIndex = columnIndex - 3;
                return;
            }
        }

        /// <summary>
        /// Determines the right lower bound for a down diagnal test  given coordinates of the last dropped token
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rightLowerRowIndex"></param>
        /// <param name="rightLowerColumnIndex"></param>
        protected void SetRightLowerCoordinateForDownDiagonalTest(int rowIndex, int columnIndex, out int rightLowerRowIndex, out int rightLowerColumnIndex)
        {
            rightLowerRowIndex = rowIndex;
            rightLowerColumnIndex = columnIndex;

            int rowUpperBound = this.Rows - 1;
            int columnUpperBound = this.Columns - 1;

            if (rowIndex == rowUpperBound || columnIndex == columnUpperBound)
                return;

            else if (rowIndex == rowUpperBound - 1 || columnIndex == columnUpperBound - 1)
            {
                rightLowerRowIndex = rowIndex + 1;
                rightLowerColumnIndex = columnIndex + 1;
                return;
            }

            else if (rowIndex == rowUpperBound - 2 || columnIndex == columnUpperBound - 2)
            {
                rightLowerRowIndex = rowIndex + 2;
                rightLowerColumnIndex = columnIndex + 2;
                return;
            }

            else if (rowIndex <= rowUpperBound - 3 || columnIndex <= columnUpperBound - 3)
            {
                rightLowerRowIndex = rowIndex + 3;
                rightLowerColumnIndex = columnIndex + 3;
                return;
            }
        }

        /// <summary>
        /// Determines the left lower bound for a down diagnal test  given coordinates of the last dropped token
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="leftLowerRowIndex"></param>
        /// <param name="leftLowerColumnIndex"></param>
        protected void SetLeftLowerCoordinateForUpDiagonalTest(int rowIndex, int columnIndex, out int leftLowerRowIndex, out int leftLowerColumnIndex)
        {
            //intialize
            leftLowerRowIndex = rowIndex;
            leftLowerColumnIndex = columnIndex;

            int rowUpperBound = this.Rows - 1;

            if (rowIndex == rowUpperBound || columnIndex == 0)
                return;

            else if (rowIndex == rowUpperBound - 1 || columnIndex == 1)
            {
                leftLowerRowIndex = rowIndex + 1;
                leftLowerColumnIndex = columnIndex - 1;
                return;
            }

            else if (rowIndex == rowUpperBound - 2 || columnIndex == 2)
            {
                leftLowerRowIndex = rowIndex + 2;
                leftLowerColumnIndex = columnIndex - 2;
                return;
            }

            else if (rowIndex <= rowUpperBound - 3 || columnIndex >= 3)
            {
                leftLowerRowIndex = rowIndex + 3;
                leftLowerColumnIndex = columnIndex - 3;
                return;
            }
        }
        
        /// <summary>
        /// Determines the right upper bound for a down diagnal test given coordinates of the last dropped token
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rightUpperRowIndex"></param>
        /// <param name="rightUpperColumnIndex"></param>
        protected void SetRightUpperCoordinateForUpDiagonalTest(int rowIndex, int columnIndex, out int rightUpperRowIndex, out int rightUpperColumnIndex)
        {
            //intialize
            rightUpperRowIndex = rowIndex;
            rightUpperColumnIndex = columnIndex;

            int columnUpperBound = this.Columns - 1;

            if (rowIndex == 0 || columnIndex == columnUpperBound)
                return;

            else if (rowIndex == 1 || columnIndex == columnUpperBound - 1)
            {
                rightUpperRowIndex = rowIndex - 1;
                rightUpperColumnIndex = columnIndex + 1;
                return;
            }

            else if (rowIndex == 2 || columnIndex == columnUpperBound - 2)
            {
                rightUpperRowIndex = rowIndex - 2;
                rightUpperColumnIndex = columnIndex + 2;
                return;
            }

            else if (rowIndex >= 3 || columnIndex <= columnUpperBound - 3)
            {
                rightUpperRowIndex = rowIndex - 3;
                rightUpperColumnIndex = columnIndex + 3;
                return;
            }
        }

        #endregion

    }
}

