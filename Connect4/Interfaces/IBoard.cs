
namespace Connect4.Interfaces
{
    public interface IBoard
    {
        bool CheckForWin();
        bool IsUserMoveValid(int playersMove);
    }
}
