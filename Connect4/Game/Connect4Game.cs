﻿using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;

namespace Connect4.Game
{
    public abstract class Connect4Game : IGame
    {
        public IBoard Board
        {
            get;
            protected set;
        }

        public IDataDevice DataDevice
        {
            get;
            protected set;
        }

        public IList<IPlayer> Players
        {
            get; set;
        }

        public Connect4Game() { }

        //creates a two player game and sets the players name
        public Connect4Game(IBoard board, IDataDevice dataDevice)
        {
            Board = board ?? throw new ArgumentNullException("board");
            DataDevice = dataDevice ?? throw new ArgumentNullException("dataDevice");
        }

        /// <summary>
        /// Implements IGame.Play(): Toggles between the two players, displays their move, and test for a win
        /// </summary>
        public virtual void Play()
        {
            SetPlayerNames();

            //Display initial board.
            DataDevice.WriteData(Board.ToString());        
            DataDevice.WriteData(String.Empty);
            DataDevice.WriteData("Let's Begin!!!");

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
                DataDevice.WriteData(String.Empty);
                DataDevice.WriteData(Board.ToString());

                //check for win or draw.
                if (this.Board.CheckForWin(column))
                {
                    DataDevice.WriteData(string.Format("*********  {0} WON...Congratulations!!!", player.Name.ToUpper()));
                    break;
                }
                else if (i == totalMoves)
                {
                    DataDevice.WriteData("The board is filled and the game is a draw...Sorry no winner.");
                    break;
                }
            }

        }

        /// <summary>
        /// Set the player's names given input from users
        /// </summary>
        protected void SetPlayerNames()
        {
            //Red
            var player1 = Players.FirstOrDefault(x => x.Token == Token.Red);
            if (String.IsNullOrEmpty(player1.Name))
            {
                DataDevice.WriteData("What is the Player1's name?");
                player1.Name = DataDevice.ReadData();
            }

            //Yellow
            var player2 = Players.FirstOrDefault(x => x.Token == Token.Yellow);
            if (String.IsNullOrEmpty(player2.Name))
            {
                DataDevice.WriteData("What is the Player2's name?");
                player2.Name = DataDevice.ReadData();
            }
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


