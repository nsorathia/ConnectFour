﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4.Interfaces
{
    public interface IGame
    {
        IList<IPlayer> Players { get; set; }
        void Play();
    }
}
