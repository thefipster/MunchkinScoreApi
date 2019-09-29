using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class FightStartActionTest
    {
        [Fact]
        public void StartingAFightWhenGameIsNotStarted_ThrowsInvalidActionException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var hero = new Hero(PlayerFactory.CreateMale("GI Joe"));
            var monster = new Monster("Plastiksoldaten", 2);
            var startFight = FightStartMessage.Create(1, hero.Player.Id, monster);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, startFight));
        }

        [Fact]
        public void StartingAFightWhileAnotherFightIsNotEnded_ThrowsInvalidActionException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var monster = new Monster("Plastiksoldaten", 2);
            var startFight = FightStartMessage.Create(sequence.Next, playerId, monster);
            var startAnotherFight = FightStartMessage.Create(sequence.Next, playerId, monster);

            // Act & Assert
            quest.AddMessage(gameId, startFight);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, startAnotherFight));
        }
    }
}
