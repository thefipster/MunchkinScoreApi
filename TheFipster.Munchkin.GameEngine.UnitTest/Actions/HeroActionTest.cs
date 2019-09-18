using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class HeroActionTest
    {
        [Fact]
        public void AddHeroTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var hero = HeroFactory.CreateFemaleHero("Bonnie"); 
            var addHero = new HeroMessage(gameId, hero, Modifier.Add);

            var score = quest.AddMessage(addHero);

            Assert.Single(score.Heroes);
            Assert.Equal("Bonnie", score.Heroes.First().Player.Name);
            Assert.Equal("female", score.Heroes.First().Player.Gender);
        }
        
        [Fact]
        public void AddHeroAndUndoTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var hero = HeroFactory.CreateFemaleHero("Bonnie"); 
            var addHero = new HeroMessage(gameId, hero, Modifier.Add);

            quest.AddMessage(addHero);
            var score = quest.Undo(gameId);

            Assert.Empty(score.Heroes);
        }
        
        [Fact]
        public void AddTwoHeroesTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var bonnie = HeroFactory.CreateFemaleHero("Bonnie"); 
            var clyde = HeroFactory.CreateMaleHero("Clyde"); 
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);
            var addClyde = new HeroMessage(gameId, clyde, Modifier.Add);

            quest.AddMessage(addBonnie);
            var score = quest.AddMessage(addClyde);

            Assert.Equal(2, score.Heroes.Count);
        }
        
        [Fact]
        public void AddTwoHeroesAndUndoTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var bonnie = HeroFactory.CreateFemaleHero("Bonnie"); 
            var clyde = HeroFactory.CreateMaleHero("Clyde"); 
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
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var bonnie = HeroFactory.CreateFemaleHero("Bonnie"); 
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);

            quest.AddMessage(addBonnie);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addBonnie));
        }

        [Fact]
        public void RemoveUnknownHeroThrowsExceptionTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var bonnie = HeroFactory.CreateFemaleHero("Bonnie"); 
            var removeBonnie = new HeroMessage(gameId, bonnie, Modifier.Remove);

            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeBonnie));
        }
        
        [Fact]
        public void AddHeroAndRemoveHeroAndUndoTest()
        {
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var bonnie = HeroFactory.CreateFemaleHero("Bonnie"); 
            var addBonnie = new HeroMessage(gameId, bonnie, Modifier.Add);
            var removeBonnie = new HeroMessage(gameId, bonnie, Modifier.Remove);

            quest.AddMessage(addBonnie);
            quest.AddMessage(removeBonnie);
            var score = quest.Undo(gameId);

            Assert.Single(score.Heroes);
            Assert.Equal("Bonnie", score.Heroes.First().Player.Name);
            Assert.Equal("female", score.Heroes.First().Player.Gender);
        }
    }
}
