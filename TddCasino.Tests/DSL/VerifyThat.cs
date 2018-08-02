using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace TddCasino.Tests.DSL
{
    public static class VerifyThat
    {
        public static void GetWinCoefficientCalledOnceIn(Mock<Game> gameMock)
        {
            gameMock.Verify(x => x.GetWinCoefficient(4), Times.Once);
        }

        public static void LoseCalledOnceIn(Mock<Player> playerMock)
        {
            playerMock.Verify(x => x.LoseChips(It.IsAny<int>()), Times.Once);
        }

        public static void WinCalledWithBetWith60ChipsOnceIn(Mock<Player> playerMock)
        {
            playerMock.Verify(x => x.WinChips(60), Times.Once);
        }
    }
}
