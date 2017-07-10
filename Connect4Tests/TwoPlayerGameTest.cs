using System;
using System.Linq;
using Connect4;
using Connect4.Interfaces;
using Connect4.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Connect4.Board;

namespace Connect4Tests
{
    [TestClass]
    public class TwoPlayerGameTest : TwoPlayerGame
    {
        public TwoPlayerGameTest() : base() { }

        public TwoPlayerGameTest(IBoard board, IDataDevice dataDevice)
            : base(board, dataDevice) { }

        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullBoard()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();

            new TwoPlayerGame(null, dataDevice.Object);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullDataDevice()
        {
            new TwoPlayerGame(new Connect4Board(), null);
        }

        [TestMethod]
        public void SetPlayerNames_ShouldUpdatePlayerNamePropertyWithUserDataInput()
        {
            //Mock DataDevice
            Mock<IBoard> board = new Mock<IBoard>();
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            dataDevice.Setup(x => x.ReadLine()).Returns("Frodo");
            dataDevice.Setup(x => x.WriteLine(It.IsAny<string>()));

            var game = new TwoPlayerGameTest(board.Object, dataDevice.Object);

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

            var game = new TwoPlayerGameTest(board.Object, dataDevice.Object);

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
