﻿using System;
using Connect4.Interfaces;

namespace Connect4.Player
{
    public class Connect4Player : Player
    {

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
                this.DataDevice.WriteData(string.Format("...{0}, please enter a column from 1 to {1}.", this.Name, iBoard.Columns));
                var userChoice = this.DataDevice.ReadData();

                //validate userchoice is integer
                if (!Int32.TryParse(userChoice, out column))
                {
                    this.DataDevice.WriteData("...You must enter a numeric value. Try again.");
                    continue;
                }

                //validate user choice is within bounds and that the column is not full
                if (!iBoard.IsUserMoveValid(column - 1))
                {
                    this.DataDevice.WriteData("...Try another column, your choice is not valid");
                    column = 0;  //reset 
                }
            }

            return column;
        }

    }
}




