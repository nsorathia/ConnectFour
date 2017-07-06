
namespace Connect4.Interfaces
{
    public interface IBoard
    {
        int Columns { get; set; }
        int Rows { get; set; }

        bool CheckForWin(int lastUserMove);
        bool IsUserMoveValid(int userMove);
        void SetUserMove(int move, Token token);
    }
}
