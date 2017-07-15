using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;
using Connect4.Player;

namespace Connect4.Game
{
    public class TwoPlayerGame : Connect4Game
    {
                
        public TwoPlayerGame() { }

        //creates a two player game and sets the players name
        public TwoPlayerGame(IBoard board, IDataDevice dataDevice, IPlayer player1, IPlayer player2)
            : base(board, dataDevice)
        {
            player1.Token = Token.Red;
            player2.Token = Token.Yellow;

            Players = new List<IPlayer>() { player1, player2 };
        }

        

    }
}

