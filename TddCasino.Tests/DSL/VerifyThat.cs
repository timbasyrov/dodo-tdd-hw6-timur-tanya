using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TddCasino.Tests.DSL
{
    public static class VerifyThat
    {
        public static void GetWinCoefficientCallOnceIn(Mock<Game> gameMock)
        {
            gameMock.Verify(x => x.GetWinCoefficient(4), Times.Once);
        }

        public static void LoseCallOnceIn(Mock<Player> playerMock)
        {
            playerMock.Verify(x => x.Lose(), Times.Once);
        }
    }
}
