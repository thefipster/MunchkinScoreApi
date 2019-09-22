using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DungeonController : ControllerBase
    {
        private readonly ICardStore _cardStore;

        public DungeonController(ICardStore cardStore)
        {
            _cardStore = cardStore;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var dungeons = _cardStore.GetDungeons();
            return Ok(dungeons);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post(string dungeon)
        {
            _cardStore.SyncDungeons(new List<string> { dungeon });
            var url = Url.Action(nameof(Get));
            return Created(url, dungeon);
        }
    }
}