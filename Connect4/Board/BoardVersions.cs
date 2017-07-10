using System;
using System.Collections.Generic;
using System.Text;
using Connect4.Interfaces;

namespace Connect4.Board
{
    public class BoardVersion
    {
        public IBoard Board
        {
            get;
            private set;
        }
        
        /// <summary>
        /// A snapshot of the board with aan additional move by Opposite token
        /// </summary>
        public IList<BoardVersion> OppMoveVersions
        {
            get;
            private set;
        }

        public int Points
        {
            get; set;
        }

        public BoardVersion(IBoard board)
        {
            this.Board = board;
            this.OppMoveVersions = new List<BoardVersion>();
            this.Points = int.MinValue;
        }
    }

}
