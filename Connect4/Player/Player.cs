﻿using Connect4.Interfaces;

namespace Connect4.Player
{
    public abstract class Player : IPlayer
    {
        public IDataDevice DataDevice
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Token Token
        {
            get;
            set;
        }

        public Player() {}

        public Player(IDataDevice datadevice)
        {
            DataDevice = datadevice;
        }

        public abstract int Move(IBoard board);

    }


}
