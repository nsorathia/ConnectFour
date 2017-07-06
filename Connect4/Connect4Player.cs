using System;
using Connect4.Interfaces;

namespace Connect4
{
    public class Connect4Player : Player
    {
        public Connect4Player() { }

        public override int Move(IBoard iBoard)
        {
            if (iBoard == null)
                throw new ArgumentNullException("iBoard");

            var c4Board = iBoard as Connect4Board;
            int column = 0;

            while (column < 1 || column > c4Board.Grid.GetLength(1))
            {
                var userChoice = GetUserMove(c4Board);

                if (!IsNumeric(userChoice, out column))
                    continue;

                if (!c4Board.IsUserMoveValid(column - 1))
                    Console.WriteLine("...The column you chose is full. Try another");
            }

            return column;
        }


        /// <summary>
        /// Reads the raw user's input
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private string GetUserMove(Connect4Board c4Board)
        {
            Console.WriteLine(string.Format("...{0}, please enter a column from 1 to {1}.", this.Name, c4Board.Grid.GetLength(1)));
            return Console.ReadLine();
        }

        private bool IsNumeric(string input, out int column)
        {
            column = 0;
            bool isNumeric = Int32.TryParse(input, out column);

            if (!isNumeric)
                Console.WriteLine("...You must enter a numeric value. Try again.");

            return isNumeric;
        }




    }
}




