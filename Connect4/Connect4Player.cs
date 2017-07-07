using System;
using Connect4.Interfaces;

namespace Connect4
{
    public class Connect4Player : Player
    {
        public Connect4Player() : base() { }
        public Connect4Player(IDataDevice datadevice) : base(datadevice) { }

        /// <summary>
        /// Implements IBoard.Move : Validates user input and returns the
        /// column number of where the user wants to drop her token 
        /// </summary>
        /// <param name="iBoard"></param>
        /// <returns>int</returns>
        public override int Move(IBoard iBoard)
        {
            if (iBoard == null)
                throw new ArgumentNullException("iBoard");

            int column = 0;
            while (column < 1 || column > iBoard.Columns)
            {
                //Get User column Choice
                this.DataDevice.WriteLine(string.Format("...{0}, please enter a column from 1 to {1}.", this.Name, iBoard.Columns));
                var userChoice = this.DataDevice.ReadLine();

                //validate userchoice is integer
                if (!Int32.TryParse(userChoice, out column))
                {
                    this.DataDevice.WriteLine("...You must enter a numeric value. Try again.");
                    continue;
                }                    

                //validate user choice is within bounds and that the column is not full
                if (!iBoard.IsUserMoveValid(column - 1))
                    this.DataDevice.WriteLine("...The column you chose is full. Try another");
            }

            return column;
        }

    }
}




