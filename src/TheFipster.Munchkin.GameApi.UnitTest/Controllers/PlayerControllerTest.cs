using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheFipster.Munchkin.GameApi.Controllers;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameStorage.Volatile;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.GameApi.UnitTest.Controllers
{
    public class PlayerControllerTest
    {
        private GameMaster _bonnie;
        private GameMaster _clyde;
        private VolatilePlayerStore _playerStore;
        private PlayerController _controller;
        private string _externalId;
        private string _userName;
        private Player _jekyll;
        private Player _hyde;

        public PlayerControllerTest()
        {
            _jekyll = PlayerFactory.CreateMale("Dr. Jekyll");
            _hyde = PlayerFactory.CreateMale("Mr. Hyde");

            _bonnie = new GameMaster(
                PlayerFactory.CreateFemale("Bonnie"), "bonnie@renegades.com");
            _bonnie.PlayerPool.Add(_jekyll);
            _bonnie.PlayerPool.Add(_hyde);

            _clyde = new GameMaster(
                PlayerFactory.CreateMale("Clyde"), "clyde@renegades.com");

            _playerStore = new VolatilePlayerStore();
            _playerStore.Add(_bonnie);
            _playerStore.Add(_clyde);


            _externalId = _bonnie.ExternalId;
            _userName = _bonnie.Name;

            _controller = new PlayerController(_playerStore);
            _controller.ControllerContext =
                ControllerContextFactory.CreateWithUserContext(_externalId, _userName);
        }

        [Fact]
        public void CallGetAction_ResultsInBonnieBeingReturned_Test()
        {
            var result = _controller.GetOrCreate() as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<GameMaster>().Subject.Name.Should().Be(_bonnie.Name);
        }

        [Fact]
        public void CallGetPoolAction_ResultsInBonniesPoolWithJekyllAndHydeBeingReturned_Test()
        {
            var result = _controller.GetPool() as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<Player>>().Subject.Should().HaveCount(2);
        }
    }
}
