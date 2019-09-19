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
        private Hero bonnie = HeroFactory.CreateFemaleHero("Bonnie");
        private Hero clyde = HeroFactory.CreateMaleHero("Clyde");

        [Fact]
        public void AddHeroTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = new HeroMessage(gameId, bonnie, Modifier.Add);

            var score = quest.AddMessage(addHero);

            Assert.Single(score.Heroes);
            Assert.Equal(bonnie.Player.Name, score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Player.Gender, score.Heroes.First().Player.Gender);
        }
        
        [Fact]
        public void AddHeroAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = new HeroMessage(gameId, bonnie, Modifier.Add);

            quest.AddMessage(addHero);
            var score = quest.Undo(gameId);

            Assert.Empty(score.Heroes);
        }
        
        [Fact]
        public void AddTwoHeroesTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);
            var addClyde = new HeroMessage(gameId, clyde, Modifier.Add);

            quest.AddMessage(addBonnie);
            var score = quest.AddMessage(addClyde);

            Assert.Equal(2, score.Heroes.Count);
        }
        
        [Fact]
        public void AddTwoHeroesAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);
            var addClyde = new HeroMessage(gameId, clyde, Modifier.Add);

            quest.AddMessage(addBonnie);
            quest.AddMessage(addClyde);
            var score = quest.Undo(gameId);

            Assert.Single(score.Heroes);
        }
        
        [Fact]
        public void AddSameHeroTwiceThrowsExceptionTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);

            quest.AddMessage(addBonnie);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addBonnie));
        }

        [Fact]
        public void RemoveUnknownHeroThrowsExceptionTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeBonnie = new HeroMessage(gameId, bonnie, Modifier.Remove);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeBonnie));
        }
        
        [Fact]
        public void AddHeroAndRemoveHeroAndUndoTest()
        {
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);
            var removeBonnie = new HeroMessage(gameId, bonnie, Modifier.Remove);

            quest.AddMessage(addBonnie);
            quest.AddMessage(removeBonnie);
            var score = quest.Undo(gameId);

            Assert.Single(score.Heroes);
            Assert.Equal(bonnie.Player.Name, score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Player.Gender, score.Heroes.First().Player.Gender);
        }
    }
}
