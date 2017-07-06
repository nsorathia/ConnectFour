using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;

namespace Connect4
{
    public class TwoPlayerGame : IGame
    {
        public IBoard Board
        {
            get;
            private set;
        }

        public IDataDevice DataDevice
        {
            get;
            private set;
        }

        /// <summary>
        /// Implements IGame.Players 
        /// </summary>
        public IList<IPlayer> Players
        {
            get; set;
        }
                
        public TwoPlayerGame() { }

        //creates a two player game and sets the players name
        public TwoPlayerGame(IBoard board, IDataDevice dataDevice, IPlayer player1, IPlayer player2)
        {
            Board = board ?? throw new ArgumentNullException("board");
            DataDevice = dataDevice ?? throw new ArgumentNullException("dataDevice");
            
            var p1 = player1 ?? throw new ArgumentNullException("player1");
            p1.Token = Token.Red;

            var p2 = player2 ?? throw new ArgumentNullException("player2");
            p2.Token = Token.Yellow;

            Players = new List<IPlayer>() { p1, p2 };

            SetPlayerNames();

        }

        /// <summary>
        /// Implements IGame.Play(): Toggles between the two players, displays their move, and test for a win
        /// </summary>
        public void Play()
        {
            //Display initial board.
            DataDevice.WriteLine(Board.ToString());
            DataDevice.WriteLine("Let's Begin!!!");

            int totalMoves = Board.Rows * Board.Columns;
            for (int i = 1; i <= totalMoves; i++)
            {
                //get Player
                var player = GetPlayer(i);
                
                //get their Move
                int column = player.Move(this.Board);

                //update the board
                this.Board.SetUserMove(column, player.Token);
                
                //display the board.
                DataDevice.WriteLine(Board.ToString());

                //check for win or draw.
                if (this.Board.CheckForWin(column))
                {
                    DataDevice.WriteLine(string.Format("*********  {0} WON...Congratulations!!!", player.Name.ToUpper()));
                    break;
                }
                else if (i == totalMoves)
                {
                    DataDevice.WriteLine("The board is filled and the game is a draw...Sorry no winner.");
                    break;
                }
            }

        }

        /// <summary>
        /// Set the player's names given input from users
        /// </summary>
        protected void SetPlayerNames()
        {
            //Yellow
            var player1 = Players.FirstOrDefault(x => x.Token == Token.Red);
            DataDevice.WriteLine("What is the Player1's name?");
            player1.Name = DataDevice.ReadLine();

            //Red
            var player2 = Players.FirstOrDefault(x => x.Token == Token.Yellow);
            DataDevice.WriteLine("What is the Player2's name?");
            player2.Name = DataDevice.ReadLine();
        }

        /// <summary>
        /// Returns the appropriate player given the game movecount.
        /// </summary>
        /// <param name="moveCount"></param>
        /// <returns></returns>
        protected IPlayer GetPlayer(int moveCount)
        {
            //Check if even
            if (moveCount % 2 == 0)
                return Players.FirstOrDefault(x => x.Token == Token.Yellow);

            else // odd
                return Players.FirstOrDefault(x => x.Token == Token.Red);
        }

    }
}

