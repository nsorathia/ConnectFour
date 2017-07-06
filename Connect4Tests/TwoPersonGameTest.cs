using System;
using System.Linq;
using Connect4;
using Connect4.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Connect4Tests
{
    [TestClass]
    public class TwoPersonGameTest : TwoPersonGame
    {
        public TwoPersonGameTest() : base() { }

        public TwoPersonGameTest(IBoard board, IDataDevice dataDevice, IPlayer player1, IPlayer player2)
            : base(board, dataDevice, player1, player2) { }

        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullBoard()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();

            new TwoPersonGame(null, dataDevice.Object, new Connect4Player(dataDevice.Object), new Connect4Player(dataDevice.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullPlayer1()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();

            new TwoPersonGame(new Connect4Board(), dataDevice.Object, null, new Connect4Player(dataDevice.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullPlayer2()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();

            new TwoPersonGame(new Connect4Board(), dataDevice.Object, new Connect4Player(dataDevice.Object), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGame_ConstructorShouldNotAllowNullDataDevice()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();

            new TwoPersonGame(new Connect4Board(), null, new Connect4Player(dataDevice.Object), new Connect4Player(dataDevice.Object));
        }

        [TestMethod]
        public void SetPlayerNames_ShouldUpdatePlayerNamePropertyWithUserDataInput()
        {
            //Mock DataDevice
            Mock<IBoard> board = new Mock<IBoard>();
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            dataDevice.Setup(x => x.ReadLine()).Returns("Frodo");
            dataDevice.Setup(x => x.WriteLine(It.IsAny<string>()));

            var game = new TwoPersonGameTest(board.Object, dataDevice.Object, new Connect4Player(dataDevice.Object), new Connect4Player(dataDevice.Object));

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

            var game = new TwoPersonGameTest(board.Object, dataDevice.Object, new Connect4Player(dataDevice.Object), new Connect4Player(dataDevice.Object));

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
