using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class FightEndActionTest
    {
        private string badThings = "Schlimme Dinge";

        [Fact]
        public void EndingANotStartedFight_ThrowsInvalidActionException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var hero = new Hero(PlayerFactory.CreateMale("GI Joe"));
            var endFight = FightEndMessage.Create(sequence.Next, badThings);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, endFight));
        }
    }
}
