using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuest _quest;

        public GameController(IQuest quest)
        {
            _quest = quest;
        }

        [Authorize]
        [HttpGet("new")]
        public ActionResult NewGame()
        {
            var gameId = _quest.StartJourney();
            var url = Url.Action(nameof(AddMessage));
            return Created(url, gameId);
        }

        [Authorize]
        [HttpPost("append")]
        public ActionResult AddMessage([FromBody] GameMessage message)
        {
            var score = _quest.AddMessage(message);
            return Ok(score);
        }
    }
}