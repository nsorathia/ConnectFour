using System;
using Connect4.Interfaces;

namespace Connect4
{
    public class TwoPersonGame : IGame
    {
        public IPlayer Player1
        {
            get;
            private set;
        }

        public IPlayer Player2
        {
            get;
            private set;
        }

        public IBoard Board
        {
            get;
            private set;
        }

        public TwoPersonGame(IBoard board, IPlayer player1, IPlayer player2)
        {
            Board = board ?? throw new ArgumentNullException("board");
            Player1 = player1 ?? throw new ArgumentNullException("player1");
            Player2 = player2 ?? throw new ArgumentNullException("player2");
        }
        
        public void Play()
        {
            if (this.Board == null)
                throw new NullReferenceException("Board property object cannot be null");

            var board = this.Board as Connect4Board;
            if (board == null)
                throw new NullReferenceException("Board object must be of type Connect4Board");


        }


    }
}

