using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Moves
{
    public class FightTest
    {
        private Monster zerschmetterling = new Monster("Zerschmetterling", 5);

        private string badThings = "Schlimme Dinge";

        [Fact]
        public void StartFight_ResultingInASetFightState_Test()
        {
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var game = quest.GetState(gameId);
            var hero = game.GetHero(playerId);

            var startFight = FightStartMessage.Create(sequence + 1, hero, zerschmetterling);

            game = quest.AddMessage(gameId, startFight);

            Assert.NotNull(game.Score.Fight);
            Assert.Equal(zerschmetterling.Name, game.Score.Fight.Monsters.First().Name);
            Assert.Equal(hero.Player.Id, game.Score.Fight.Heroes.First().Player.Id);
        }

        [Fact]
        public void StartAndEndAFight_ResultingInAnEmptyFightState_Test()
        {
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var game = quest.GetState(gameId);
            var hero = game.GetHero(playerId);

            var startFight = FightStartMessage.Create(sequence + 1, hero, zerschmetterling);
            var endFight = FightEndMessage.Create(sequence + 2, badThings);

            quest.AddMessage(gameId, startFight);
            game = quest.AddMessage(gameId, endFight);

            Assert.Null(game.Score.Fight);
        }
    }
}
