using System;
using System.Linq;
using Connect4;
using Connect4.Interfaces;
using Connect4.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Connect4.Player;
using Connect4.Board;

namespace Connect4Tests
{
    [TestClass]
    public class TwoPlayerGameTest : TwoPlayerGame
    {
        public TwoPlayerGameTest() : base() { }

        public TwoPlayerGameTest(IBoard board, IDataDevice dataDevice, IPlayer player1, IPlayer player2)
            : base(board, dataDevice, player1, player2) { }

        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullBoard()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            Mock<IPlayer> player1 = new Mock<IPlayer>();
            Mock<IPlayer> player2 = new Mock<IPlayer>();

            new TwoPlayerGame(null, dataDevice.Object, player1.Object, player2.Object);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullDataDevice()
        {
            Mock<IPlayer> player1 = new Mock<IPlayer>();
            Mock<IPlayer> player2 = new Mock<IPlayer>();
            new TwoPlayerGame(new Connect4Board(), null, player1.Object, player2.Object);
        }

        [TestMethod]
        public void SetPlayerNames_ShouldUpdatePlayerNamePropertyWithUserDataInput()
        {
            //Mock DataDevice
            Mock<IBoard> board = new Mock<IBoard>();
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            var player1 = new Connect4Player(dataDevice.Object);
            var player2 = new Connect4Player(dataDevice.Object);
            dataDevice.Setup(x => x.ReadData()).Returns("Frodo");
            dataDevice.Setup(x => x.WriteData(It.IsAny<string>()));

            var game = new TwoPlayerGameTest(board.Object, dataDevice.Object, player1, player2);

            int playerCountWithNames = game.Players.Where(x => !String.IsNullOrEmpty(x.Name)).Count();
            Assert.IsTrue(playerCountWithNames == 0);
                
            game.SetPlayerNames();

            playerCountWithNames = game.Players.Where(x => !String.IsNullOrEmpty(x.Name)).Count();
            Assert.IsTrue(playerCountWithNames == 2);
        }

        [TestMethod]
        public void GetPlayer_ShouldReturnCorrectPlayerGivenMoveCount()
        {
            //Mock DataDevice
            Mock<IBoard> board = new Mock<IBoard>();
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            var player1 = new Connect4Player(dataDevice.Object);
            var player2 = new Connect4Player(dataDevice.Object);

            var game = new TwoPlayerGameTest(board.Object, dataDevice.Object, player1, player2);

            //Get random movecount form 1 to 42
            var rnd = new Random(DateTime.Now.Millisecond);
            int moveCount = rnd.Next(1, 42);
            IPlayer player = game.GetPlayer(moveCount);

            //Test
            if (moveCount % 2 == 0) //even = player2
                Assert.IsTrue(player.Token == Token.Yellow);

            else //odd = player1
                Assert.IsTrue(player.Token == Token.Red);
        }


    }
}
