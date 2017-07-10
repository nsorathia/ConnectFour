using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4.Interfaces
{
    public interface IAlgorithm
    {
        int LevelOfDifficulty { get; set; }
        int CalculateBestMove(IBoard board, Token token);
    }
}
