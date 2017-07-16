using System;
using System.Linq;
using System.Collections.Generic;
using Connect4.Interfaces;
using Connect4.Board;

namespace Connect4.Algorithm
{
    /// <summary>
    /// Algorithm thats scores moves for two player games 
    /// which attempts to assign a min or max score for the move 
    /// given the depth of move graph.
    /// 
    /// </summary>
    public class MinMaxAlgorithm : IAlgorithm
    {

        private Random _random;
        
        #region Cnstrs

        public MinMaxAlgorithm()
            : this(ChallengeLevel.Hard) //5
        {         
        }

        public MinMaxAlgorithm(int challengeLevel) 
            : this((ChallengeLevel)challengeLevel)
        {            
        }

        public MinMaxAlgorithm(ChallengeLevel challengeLevel)
        {
            LevelOfDifficulty = (int)challengeLevel;
            _random = new Random(DateTime.Now.Millisecond);
        }

        #endregion

        #region IAgorithm implementation

        /// <summary>
        /// Implements IAlgorithm.LevelOfDifficulty: Integer value denoting level of recursion for calculating best move
        /// </summary>
        public int LevelOfDifficulty
        {
            get;
            set;
        }

        /// <summary>
        /// Implements IAlgorithm.CalculateBestMove: Utilizes an algorithm to determine the best move for the token 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="token"></param>
        /// <returns>int: column number of the best move</returns>
        public virtual int CalculateBestMove(IBoard board, Token token)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (token == Token.Empty)
                throw new ArgumentException("The token passed must be either red or yellow");

            //Collection of all available colunmns which are not full
            var availableMoves = board.GetAvailableMoves();

            //create a version of the board per available move
            var newVersions = new BoardVersion[availableMoves.Count];

            for (int i = 0; i < availableMoves.Count; i++)
            {
                //create a new Board version with an empty graph of possible plays
                newVersions[i] = CreateBoardVersion(board, availableMoves[i], token);

                //recursively creates board version graph N levels deep.
                GraphVersions(newVersions[i], GetOpposingToken(token), this.LevelOfDifficulty);

                //Calculate the score for each move given the graph of the boardVersion.
                newVersions[i].Points = CalculateScore(newVersions[i], this.LevelOfDifficulty);
            }

            int bestMove = GetIndexOfBestScoredMove(newVersions) + 1;

            return bestMove;
        }

        #endregion IAgorithm implementation
 
        /// <summary>
        /// Recursively creates a graph of N possible plays for 
        /// opposing tokens.  
        /// </summary>
        /// <param name="boardVersion"></param>
        /// <param name="token"></param>
        /// <param name="level">number of recursion levels/depth of play</param>
        protected virtual void GraphVersions(BoardVersion boardVersion, Token token, int level)
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
            for (int i=0; i < availableMoves.Count; i++)
            {
                var clonedBoardVersion = CreateBoardVersion(boardVersion.Board, availableMoves[i], token);
                
                //TODO Test:  End graphing once a win is determined.

                //recursive call with opposing token and one level deeper.
                GraphVersions(clonedBoardVersion, GetOpposingToken(token), level - 1);

                boardVersion.OppMoveVersions.Add(clonedBoardVersion);
            }
        }

        /// <summary>
        /// Recursivly calculates a score for a board move 
        /// and expodentially decreases the scores absolute value 
        /// based on the number of moves for a win.
        /// Positive value denotes a possible win for current player, Negative value denotes a possible win for the opposing player
        /// </summary>
        /// <param name="version"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        protected virtual int CalculateScore(BoardVersion version, int level)
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
            // subtract the same points for this level.
            else if (version.OppMoveVersions.Any(x => x.Board.CheckForWin(x.Board.LastMove + 1)))
                score = -pointsforLevel;
            
            else   
            {
                //Neither player can win on this play so drill down 
                //one level and calculate score for each Opposing move                                
                foreach (var oppMove in version.OppMoveVersions)
                    score -= CalculateScore(oppMove, level - 1);
            }

            return score;
        }
        
        /// <summary>
        /// Returns an index of a move that scored highest.
        /// </summary>
        /// <param name="boardVersions"></param>
        /// <returns>int: move index</returns>
        protected int GetIndexOfBestScoredMove(BoardVersion[] boardVersions)
        {
            if (boardVersions == null)
                throw new ArgumentNullException("boardVersions");

            if (boardVersions.Length == 0)
                throw new ArgumentException("The BoarVersions must conatain at least one element");

            //iterate through and find the max pts from all board version
            int maxPts = boardVersions.Select(x => x.Points).Max();

            //iterate again and find the indexes of the versions that have the max score.
            var indexesWithMaxPts = Enumerable.Range(0, boardVersions.Length)
                    .Where(i => boardVersions[i].Points == maxPts)
                    .ToList();

            //randomly choose one of the max pt indexes
            var randomMaxIndex = indexesWithMaxPts[_random.Next(0, indexesWithMaxPts.Count)];

            return randomMaxIndex;

        }
        
        /// <summary>
        /// Returns a new BoardVersion and sets the Token to the given move.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="moveIndex"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected BoardVersion CreateBoardVersion(IBoard board, int moveIndex, Token token)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (moveIndex < 0)
                throw new ArgumentException("MoveIndex value should coorespond to a column index and con not be negative");

            if (token == Token.Empty)
                throw new ArgumentException("Token must be either Red or Yellow");


            //Clone the board and set the token on the cloned board
            var clone = board.Clone();

            clone.SetUserMove(moveIndex + 1, token);

            var version = new BoardVersion(clone);

            return version;
        }

        /// <summary>
        /// Returns the opposing player's token 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected Token GetOpposingToken(Token token)
        {
            if (token == Token.Red)
                return Token.Yellow;
            else
                return Token.Red;
        }

    }
}
