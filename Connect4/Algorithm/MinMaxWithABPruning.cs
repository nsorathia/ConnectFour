using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;
using Connect4.Board;

namespace Connect4.Algorithm
{

    /// <summary>
    /// NO OVERRIDE CHANGES YET...
    /// </summary>
    public class MinMaxWithABPruning : MinMaxAlgorithm
    {

        #region Cnstrs

        public MinMaxWithABPruning() : base() { }

        public MinMaxWithABPruning(int challengeLevel)
            : base((ChallengeLevel)challengeLevel) { }

        public MinMaxWithABPruning(ChallengeLevel challengeLevel)
            : base(challengeLevel) { }

        #endregion

        #region Override implementation

        public override int CalculateBestMove(IBoard board, Token token)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (token == Token.Empty)
                throw new ArgumentException("The token passed must be either red or yellow");

            //Collection of all available colunmns which are not full
            var availableMoves = board.GetAvailableMoves();

            //create a score Collection - per available move
            var newVersions = new BoardVersion[availableMoves.Count];

            for (int i = 0; i < availableMoves.Count; i++)
            {
                //create a new Board version with an empty graph of possible plays
                newVersions[i] = CreateBoardVersion(board, availableMoves[i], token);

                //recursively create a graph N levels deep.
                GraphVersions(newVersions[i], GetOpposingToken(token), this.LevelOfDifficulty);

                //Calculate the score for each move given the graph of the boardVersion.
                newVersions[i].Points = CalculateScore(newVersions[i], this.LevelOfDifficulty);
            }

            int bestMove = GetIndexOfBestScoredMove(newVersions) + 1;

            return bestMove;
        }

        protected override void GraphVersions(BoardVersion boardVersion, Token token, int level)
        {
            if (boardVersion == null)
                throw new ArgumentNullException("boardVersion");

            if (token == Token.Empty)
                throw new ArgumentException("The token must be either red or yellow");

            if (level < 0)
                throw new ArgumentException("the level (recursion level) can not be negative");

            if (level == 0)
                return;

            var availableMoves = boardVersion.Board.GetAvailableMoves();
            for (int i = 0; i < availableMoves.Count; i++)
            {
                var clonedBoardVersion = CreateBoardVersion(boardVersion.Board, availableMoves[i], token);

                //recursive call with opposing token and one level deeper.
                GraphVersions(clonedBoardVersion, GetOpposingToken(token), level - 1);

                boardVersion.OppMoveVersions.Add(clonedBoardVersion);
            }
        }

        protected override int CalculateScore(BoardVersion version, int level)
        {
            if (version == null)
                throw new ArgumentNullException("version");

            if (level < 0)
                throw new ArgumentException("The depthLevel can not be less than 0");

            int score = 0;

            int pointsforLevel = (int)Math.Pow(10, level);

            //check if this board version is a win.
            if (version.Board.CheckForWin(version.Board.LastMove + 1))
                score = pointsforLevel;

            //Check if the Opposing player can win in the next move, then  
            // subtract the same point level for this board's move version.
            else if (version.OppMoveVersions.Any(x => x.Board.CheckForWin(x.Board.LastMove + 1)))
                score = -pointsforLevel;

            else
            {
                //Neither player can win on this play so drill down 
                //one level and calculate score for each Opponent move                                
                foreach (var oppMove in version.OppMoveVersions)
                    score -= CalculateScore(oppMove, level - 1);
            }

            return score;
        }


        #endregion Override implementation

    }
}

