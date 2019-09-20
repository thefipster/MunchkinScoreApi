using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerStore _playerStore;

        public PlayerController(IPlayerStore playerStore)
        {
            _playerStore = playerStore;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var idClaim = User.FindFirst("userId");
            var userId = Guid.Parse(idClaim.Value);
            var player = _playerStore.Get(userId);
            return Ok(player);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(GameMaster gameMaster)
        {
            var storedGameMaster = _playerStore.Get(gameMaster.Id);
            gameMaster.PlayerPool = storedGameMaster.PlayerPool;
            _playerStore.Add(gameMaster);
            var url = Url.Action(nameof(Get));
            return Created(url, gameMaster);
        }

        [Authorize]
        [HttpPost("friend")]
        public IActionResult PostNewFriend(Player friend)
        {
            var idClaim = User.FindFirst("userId");
            var userId = Guid.Parse(idClaim.Value);
            var gameMaster = _playerStore.Get(userId);

            if (gameMaster.PlayerPool.Any(x => x.Name == friend.Name))
                return BadRequest("You already have a friend with that name.");

            friend.Id = Guid.NewGuid();
            gameMaster.PlayerPool.Add(friend);
            _playerStore.Add(gameMaster);

            var url = Url.Action(nameof(Get));
            return Created(url, gameMaster);
        }
    }
}