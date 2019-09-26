using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
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
            var dungeons = _cardStore.Get(CardCollection.Dungeons);
            return Ok(dungeons);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post(string dungeon)
        {
            _cardStore.Sync(CardCollection.Dungeons, new List<string> { dungeon });
            var url = Url.Action(nameof(Get));
            return Created(url, dungeon);
        }
    }
}