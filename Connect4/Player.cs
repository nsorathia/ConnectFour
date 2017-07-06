using Connect4.Interfaces;

namespace Connect4
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

        public Player(IDataDevice datadevice)
        {
            DataDevice = datadevice;
        }

        public abstract int Move(IBoard board);

    }


}
