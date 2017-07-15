using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;

namespace Connect4.Player
{
    public class Connect4ComputerPlayer : Player
    {
        private IAlgorithm _algorithm;
        
        public Connect4ComputerPlayer() : base() { }
        public Connect4ComputerPlayer(IDataDevice datadevice, IAlgorithm algorithm) 
            : base(datadevice)
        {
            _algorithm = algorithm;
            base.Name = "R2D2";
        }

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

            this.DataDevice.WriteData("...Thinking!...");
            
            int move = _algorithm.CalculateBestMove(iBoard, this.Token);

            this.DataDevice.WriteData("...Okay - you're turn...");

            return move;            
        }




    }
}




