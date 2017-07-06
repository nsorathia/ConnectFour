
namespace Connect4.Interfaces
{
    public interface IBoard
    {
        bool CheckForWin(int lastUserMove);
        bool IsUserMoveValid(int userMove);
    }
}
