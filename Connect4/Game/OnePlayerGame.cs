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

        //creates a one player game
        public OnePlayerGame(IBoard board, IDataDevice dataDevice, IAlgorithm algorithm, IPlayer player2)
            : base(board, dataDevice)
        {
            //TODO: Inject Connect4ComputerPlayer - dependant on new IOC 
            IPlayer player1 = new Connect4ComputerPlayer(dataDevice, algorithm);
            player1.Token = Token.Red;
            player2.Token = Token.Yellow;

            Players = new List<IPlayer>() { player1, player2 };
        }

        public override void Play()
        {
            ChallengeLevel level;
            string input = "0";

            do
            {
                this.DataDevice.WriteData("Please enter a level of difficulty. 1:Easy, 2:Medium, 3:MediumHard, 4:Hard, 5:VeryHard, 6:Expert ");
                input = this.DataDevice.ReadData();
            } while (!Enum.TryParse<ChallengeLevel>(input, out level));

            //set the level of difficulty
            Container.GetObject<IAlgorithm>().LevelOfDifficulty = Convert.ToInt32(input);
            
            base.Play();
        }


    }
}


