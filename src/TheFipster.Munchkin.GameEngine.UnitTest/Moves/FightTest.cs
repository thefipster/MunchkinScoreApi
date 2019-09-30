using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameEvents;
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

            var startFight = FightStartMessage.Create(sequence.Next, hero.Player.Id, zerschmetterling);

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

            var startFight = FightStartMessage.Create(sequence.Next, hero.Player.Id, zerschmetterling);
            var endFight = FightEndMessage.Create(sequence.Next, badThings);

            quest.AddMessage(gameId, startFight);
            game = quest.AddMessage(gameId, endFight);

            Assert.Null(game.Score.Fight);
        }

        [Fact]
        public void StartAFight_ThenAnotherPlayerJoins_ResultsInRaisedHeroStrength_Test()
        {
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var xena = new Hero(PlayerFactory.CreateFemale("Xena"));
            var game = quest.GetState(gameId);
            var hero = game.GetHero(playerId);

            var heroLevelRaise = LevelMessage.Create(sequence.Next, playerId, 1);
            var heroBonusRaise = BonusMessage.Create(sequence.Next, playerId, 1);
            var addXena = PlayerMessage.CreateAdd(sequence.Next, new[] { xena.Player });
            var xenaLevelRaise = LevelMessage.Create(sequence.Next, xena.Player.Id, 1);
            var xenaBonusRaise = BonusMessage.Create(sequence.Next, xena.Player.Id, 1);
            var startFight = FightStartMessage.Create(sequence.Next, hero.Player.Id, zerschmetterling);
            var preparation = new GameMessage[] { heroLevelRaise, heroBonusRaise, addXena, xenaLevelRaise, xenaBonusRaise, startFight };

            var xenaJoinsFight = FightJoinMessage.Create(sequence.Next, xena.Player.Id);

            quest.AddMessages(gameId, preparation);
            game = quest.AddMessage(gameId, xenaJoinsFight);

            Assert.NotNull(game.Score.Fight);
            Assert.Equal(2, game.Score.Fight.Heroes.Count);
            Assert.Equal(6, game.Score.Fight.TotalHeroesScore);
        }
    }
}
