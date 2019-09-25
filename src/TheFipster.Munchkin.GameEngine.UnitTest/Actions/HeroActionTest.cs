using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class HeroActionTest
    {
        private Player bonnie = PlayerFactory.CreateFemale("Bonnie");
        private Player clyde = PlayerFactory.CreateMale("Clyde");

        [Fact]
        public void AddHeroTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = new PlayerMessage(bonnie, Modifier.Add);

            var score = quest.AddMessage(gameId, addHero);

            Assert.Single(score.Heroes);
            Assert.Equal(bonnie.Name, score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Gender, score.Heroes.First().Player.Gender);
        }
        
        [Fact]
        public void AddHeroAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = new PlayerMessage(bonnie, Modifier.Add);

            quest.AddMessage(gameId, addHero);
            var score = quest.Undo(gameId);

            Assert.Empty(score.Heroes);
        }
        
        [Fact]
        public void AddTwoHeroesTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new PlayerMessage(bonnie, Modifier.Add);
            var addClyde = new PlayerMessage(clyde, Modifier.Add);

            quest.AddMessage(gameId, addBonnie);
            var score = quest.AddMessage(gameId, addClyde);

            Assert.Equal(2, score.Heroes.Count);
        }
        
        [Fact]
        public void AddTwoHeroesAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new PlayerMessage(bonnie, Modifier.Add);
            var addClyde = new PlayerMessage(clyde, Modifier.Add);

            quest.AddMessage(gameId, addBonnie);
            quest.AddMessage(gameId, addClyde);
            var score = quest.Undo(gameId);

            Assert.Single(score.Heroes);
        }
        
        [Fact]
        public void AddSameHeroTwiceThrowsExceptionTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new PlayerMessage(bonnie, Modifier.Add);

            quest.AddMessage(gameId, addBonnie);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addBonnie));
        }

        [Fact]
        public void RemoveUnknownHeroThrowsExceptionTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeBonnie = new PlayerMessage(bonnie, Modifier.Remove);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeBonnie));
        }
        
        [Fact]
        public void AddHeroAndRemoveHeroAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new PlayerMessage(bonnie, Modifier.Add);
            var removeBonnie = new PlayerMessage(bonnie, Modifier.Remove);

            quest.AddMessage(gameId, addBonnie);
            quest.AddMessage(gameId, removeBonnie);
            var score = quest.Undo(gameId);

            Assert.Single(score.Heroes);
            Assert.Equal(bonnie.Name, score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Gender, score.Heroes.First().Player.Gender);
        }
    }
}
