using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;
using Connect4.Player;

namespace Connect4.Game
{
    public class OnePlayerGame : Connect4Game
    {
        public OnePlayerGame() { }

        //creates a two player game and sets the players name
        public OnePlayerGame(IBoard board, IDataDevice dataDevice, IAlgorithm algorithm)
            : base(board, dataDevice)
        {
            IPlayer player1 = new Connect4Player(dataDevice);
            player1.Token = Token.Red;

            IPlayer player2 = new Connect4ComputerPlayer(dataDevice, algorithm);
            player2.Token = Token.Yellow;

            Players = new List<IPlayer>() { player1, player2 };
        }

        public override void Play()
        {
            ChallengeLevel level;
            string input = "0";

            do
            {
                this.DataDevice.WriteLine("Please enter a level of difficulty. 1:Easy, 2:Medium, 3:MediumHard, 4:Hard, 5:VeryHard, 6:Expert ");
                input = this.DataDevice.ReadLine();
            } while (!Enum.TryParse<ChallengeLevel>(input, out level));

            //set the level of difficulty
            Container.GetObject<IAlgorithm>().LevelOfDifficulty = Convert.ToInt32(input);
            
            base.Play();
        }


    }
}


