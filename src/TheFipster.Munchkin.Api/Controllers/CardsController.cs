using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardStore _cardStore;

        public CardsController(ICardStore cardStore)
        {
            _cardStore = cardStore;
        }

        [Authorize]
        [HttpGet("dungeons")]
        public ActionResult GetDungeons() => Get(CardCollection.Dungeons);

        [Authorize]
        [HttpPost("dungeons")]
        public ActionResult PostDungeon(string dungeon) => Post(CardCollection.Dungeons, dungeon, nameof(GetDungeons));

        [Authorize]
        [HttpGet("classes")]
        public ActionResult GetClasses() => Get(CardCollection.Classes);

        [Authorize]
        [HttpPost("classes")]
        public ActionResult PostClass(string @class) => Post(CardCollection.Classes, @class, nameof(GetClasses));

        [Authorize]
        [HttpGet("races")]
        public ActionResult GetRaces() => Get(CardCollection.Races);

        [Authorize]
        [HttpPost("races")]
        public ActionResult PostRace(string race) => Post(CardCollection.Races, race, nameof(GetRaces));

        [Authorize]
        [HttpGet("monsters")]
        public ActionResult GetMonsters() => Get(CardCollection.Monsters);

        [Authorize]
        [HttpPost("monsters")]
        public ActionResult PostMonsters(string monster) => Post(CardCollection.Monsters, monster, nameof(GetMonsters));

        [Authorize]
        [HttpGet("curses")]
        public ActionResult GetCurses() => Get(CardCollection.Curses);

        [Authorize]
        [HttpPost("curses")]
        public ActionResult PostCurses(string curse) => Post(CardCollection.Curses, curse, nameof(GetCurses));

        private ActionResult Get(string collection)
        {
            var cards = _cardStore.Get(collection);
            return Ok(cards);
        }

        private ActionResult Post(string collection, string newCard, string getMethod)
        {
            _cardStore.Sync(collection, new List<string> { newCard });
            var url = Url.Action(getMethod);
            return Created(url, newCard);
        }
    }
}