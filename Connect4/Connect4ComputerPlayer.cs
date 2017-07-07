using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;

namespace Connect4
{
    public class Connect4ComputerPlayer : Player
    {
        public Connect4ComputerPlayer() : base() { }
        public Connect4ComputerPlayer(IDataDevice datadevice) : base(datadevice) { }

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

            this.DataDevice.WriteLine("...Let me think....");
            System.Threading.Thread.Sleep(2000);
            this.DataDevice.WriteLine("...Okay...");

            int move = CalculateBestMove(iBoard);
            return move;
        }

        protected int CalculateBestMove(IBoard board)
        {
            var availableMoves = GetAvailableMoves(board);
            return availableMoves.First();
        }

        protected IList<int> GetAvailableMoves(IBoard board)
        {
            return new List<int> { 1, 2 };
        }


    }
}




