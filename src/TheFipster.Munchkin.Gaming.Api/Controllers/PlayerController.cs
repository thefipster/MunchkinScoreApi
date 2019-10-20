using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.Gaming.Storage;

namespace TheFipster.Munchkin.Gaming.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerStore _playerStore;

        private string ExternalId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        private string Name => User.FindFirst("name")?.Value;
        private string Email => User.FindFirst(ClaimTypes.Email)?.Value;

        public PlayerController(IPlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrCreate()
        {
            try
            {
                var gameMaster = _playerStore.GetByExternalId(ExternalId);
                return Ok(gameMaster);
            }
            catch (UnknownPlayerException)
            {
                var gameMaster = _playerStore.Register(Name, ExternalId, Email);
                var url = Url.Action(nameof(GetOrCreate));
                return Created(url, gameMaster);
            }
        }

        [Authorize]
        [HttpGet("pool")]
        public IActionResult GetPool()
        {
            var gameMaster = _playerStore.GetByExternalId(ExternalId);
            return Ok(gameMaster.PlayerPool);
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult PostProfile(GameMaster gameMaster)
        {
            var storedGameMaster = _playerStore.Get(gameMaster.Id);
            gameMaster.PlayerPool = storedGameMaster.PlayerPool;
            _playerStore.Add(gameMaster);
            var url = Url.Action(nameof(GetOrCreate));
            return Created(url, gameMaster);
        }

        [Authorize]
        [HttpPost("friend")]
        public IActionResult PostNewFriend(Player friend)
        {
            var gameMaster = _playerStore.GetByExternalId(ExternalId);
            if (gameMaster.PlayerPool.Any(x => x.Name == friend.Name))
                return BadRequest("You already have a friend with that name.");

            friend.Id = Guid.NewGuid();
            gameMaster.PlayerPool.Add(friend);
            _playerStore.Add(gameMaster);

            var url = Url.Action(nameof(GetOrCreate));
            return Created(url, gameMaster);
        }
    }
}