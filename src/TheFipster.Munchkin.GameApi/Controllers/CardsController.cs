using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameStorage;

namespace TheFipster.Munchkin.GameApi.Controllers
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
        [HttpGet("genders")]
        public ActionResult GetGenders() => Get(CardCollection.Genders);

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
        [HttpGet("curses")]
        public ActionResult GetCurses() => Get(CardCollection.Curses);

        [Authorize]
        [HttpPost("curses")]
        public ActionResult PostCurses(string curse) => Post(CardCollection.Curses, curse, nameof(GetCurses));

        [Authorize]
        [HttpGet("fight/end")]
        public ActionResult GetFightResults() => Get(CardCollection.FightResults);

        [Authorize]
        [HttpPost("fight/end")]
        public ActionResult PostFightResults(string fightResult) => Post(CardCollection.FightResults, fightResult, nameof(GetFightResults));

        [Authorize]
        [HttpGet("fight/start")]
        public ActionResult GetFightStarters() => Get(CardCollection.FightStarters);

        [Authorize]
        [HttpPost("fight/start")]
        public ActionResult PostFightStarters(string fightStarter) => Post(CardCollection.FightStarters, fightStarter, nameof(GetFightStarters));

        [Authorize]
        [HttpGet("reasons/level/increase")]
        public ActionResult GetLevelIncreaseReasons() => Get(CardCollection.LevelIncreaseReasons);

        [Authorize]
        [HttpPost("reasons/level/increase")]
        public ActionResult GetLevelIncreaseReasons(string levelIncreaseReason) => Post(CardCollection.LevelIncreaseReasons, levelIncreaseReason, nameof(GetLevelIncreaseReasons));

        [Authorize]
        [HttpGet("reasons/level/decrease")]
        public ActionResult GetLevelDecreaseReasons() => Get(CardCollection.LevelDecreaseReasons);

        [Authorize]
        [HttpPost("reasons/level/decrease")]
        public ActionResult GetLevelDecreaseReasons(string levelDecreaseReason) => Post(CardCollection.LevelDecreaseReasons, levelDecreaseReason, nameof(GetLevelDecreaseReasons));

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