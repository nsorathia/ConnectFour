using System;
using Connect4;
using Connect4.Interfaces;
using Connect4.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Connect4Tests
{
    [TestClass]
    public class Connect4PlayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Move_ThrowsExceptionIfBoardIsNull()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            dataDevice.Setup(x => x.ReadData()).Returns("4");
            dataDevice.Setup(x => x.WriteData(It.IsAny<string>()));

            var player = new Connect4Player(dataDevice.Object);

            player.Move(null);
        }


        [TestMethod]
        public void Move_ReturnsValidColumnIndex()
        {
            //Mock DataDevice
            Mock<IDataDevice> dataDevice = new Mock<IDataDevice>();
            dataDevice.Setup(x => x.ReadData()).Returns("4");
            dataDevice.Setup(x => x.WriteData(It.IsAny<string>()));

            var player = new Connect4Player(dataDevice.Object);
            
            //Mock IBoard
            Mock<IBoard> board = new Mock<IBoard>();
            board.SetupGet(x => x.Columns).Returns(7);
            board.Setup(x => x.IsUserMoveValid(It.IsAny<int>())).Returns(true);
            
            int move = player.Move(board.Object);

            Assert.IsTrue(move > 0 && move <= board.Object.Columns);
        }
        
        

    }
}
