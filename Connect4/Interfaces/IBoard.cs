
using System.Collections.Generic;

namespace Connect4.Interfaces
{
    public interface IBoard
    {

        int Columns { get; set; }
        int Rows { get; set; }
        Token[,] Grid { get; set; }
        int LastMove { get; set; }

        bool CheckForWin(int lastUserMove);
        bool IsUserMoveValid(int userMove);
        void SetUserMove(int move, Token token);
        IList<int> GetAvailableMoves();
        IBoard Clone();
    }
}
