using Connect4.Interfaces;

namespace Connect4
{
    public abstract class Player : IPlayer
    {
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

        public abstract int Move(IBoard board);

    }


}
