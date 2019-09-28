using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Munchkin.Api.Controllers;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;
using TheFipster.Munchkin.GameStorageLite;
using Xunit;

namespace TheFipster.Munchkin.Api.UnitTest.Controllers
{
    public class MonsterControllerTest
    {
        private Monster zerschmetterling = new Monster("Zerschmetterling", 8);
        private Monster laufendeNase = new Monster("Laufende Nase", 5);

        [Fact]
        public void GetMonster_ResultsInListOfAllMonsters_Test()
        {
            // Arrange
            var monsterStore = Substitute.For<IMonsterStore>();
            monsterStore.Get().Returns(new[] { zerschmetterling });
            var controller = new MonsterController(monsterStore);

            // Act
            var okResult = controller.Get() as OkObjectResult;

            // Assert
            var monsters = (IEnumerable<Monster>)okResult.Value;
            monsters.Should().ContainSingle();
        }

        [Fact]
        public void GetMonsterByName_ResultsInSpecificMonster_Test()
        {
            // Arrange
            var monsterStore = Substitute.For<IMonsterStore>();
            monsterStore.Get(zerschmetterling.Name).Returns(zerschmetterling);
            var controller = new MonsterController(monsterStore);

            // Act
            var okResult = controller.GetByName(zerschmetterling.Name) as OkObjectResult;

            // Assert
            okResult.Value.Should().BeOfType<Monster>().Which.Name.Should().Be(zerschmetterling.Name);
        }
    }
}
