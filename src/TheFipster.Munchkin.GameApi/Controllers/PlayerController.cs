using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameStorage;

namespace TheFipster.Munchkin.GameApi.Controllers
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
            var player = getLoggedInUser();
            return Ok(player);
        }

        [Authorize]
        [HttpGet("pool")]
        public IActionResult GetPool()
        {
            var player = getLoggedInUser();
            return Ok(player.PlayerPool);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(GameMaster gameMaster)
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
            var gameMaster = getLoggedInUser();
            if (gameMaster.PlayerPool.Any(x => x.Name == friend.Name))
                return BadRequest("You already have a friend with that name.");

            friend.Id = Guid.NewGuid();
            gameMaster.PlayerPool.Add(friend);
            _playerStore.Add(gameMaster);

            var url = Url.Action(nameof(Get));
            return Created(url, gameMaster);
        }

        private GameMaster getLoggedInUser()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(idClaim.Value);
            return _playerStore.Get(userId);
        }
    }
}