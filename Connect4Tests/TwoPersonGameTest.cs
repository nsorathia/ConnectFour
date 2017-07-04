using System;
using Connect4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4Tests
{
    [TestClass]
    public class TwoPersonGameTest
    {
        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void TwoPlayerGameConstructorShouldNotAllowNullBoardProperty()
        {
            new TwoPersonGame(null, new Person(), new Person());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGameConstructorShouldNotAllowNullPlayer1Property()
        {
            new TwoPersonGame(new Connect4Board(), null, new Person());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TwoPlayerGameConstructorShouldNotAllowNullPlayer2Property()
        {
            new TwoPersonGame(new Connect4Board(), new Person(), null);
        }
    }
}
