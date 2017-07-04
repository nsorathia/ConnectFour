namespace Connect4.Interfaces
{
    public interface IPlayer
    {
        Token Token { get; set; }
        string Name { get; set; }
        void Move(IBoard board);
    }
}
