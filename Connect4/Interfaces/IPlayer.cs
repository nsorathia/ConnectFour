namespace Connect4.Interfaces
{
    public interface IPlayer
    {
        Token Token { get; set; }
        string Name { get; set; }
        int Move(IBoard board);
    }
}
