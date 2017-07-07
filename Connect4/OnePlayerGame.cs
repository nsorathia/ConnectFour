using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;

namespace Connect4
{
    public class OnePlayerGame : Connect4Game
    {
        public OnePlayerGame() { }

        //creates a two player game and sets the players name
        public OnePlayerGame(IBoard board, IDataDevice dataDevice)
            : base(board, dataDevice)
        {
            IPlayer player1 = new Connect4Player(dataDevice);
            player1.Token = Token.Red;

            IPlayer player2 = new Connect4ComputerPlayer(dataDevice);
            player2.Token = Token.Yellow;

            Players = new List<IPlayer>() { player1, player2 };
        }



    }
}


