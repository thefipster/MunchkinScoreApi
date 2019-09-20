using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameOrchestrator;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IQuest _quest;
        private readonly IInitializationCache _cache;
        private readonly IInitCodePollService _initCodePolling;

        public GameController(IQuest quest, IInitializationCache cache, IInitCodePollService initCodePolling)
        {
            _quest = quest;
            _cache = cache;
            _initCodePolling = initCodePolling;
        }

        [HttpGet("init")]
        public ActionResult InitGame()
        {
            var initCode = _cache.GenerateInitCode();
            _initCodePolling.CreateWaitHandle(initCode);
            var url = Url.Action(nameof(VerifyInitCode), new { initCode });
            Response.Headers.Add("Location", url);
            return Ok(new { initCode });
        }

        [Authorize]
        [HttpGet("verify/{initCode}")]
        public ActionResult VerifyInitCode(string initCode)
        {
            if (!_cache.CheckInitCode(initCode))
                throw new InvalidInitCodeException();

            var gameId = _quest.StartJourney();
            var url = Url.Action(nameof(AddMessage));

            _initCodePolling.FinishCodePollRequest(initCode, gameId);
            Response.Headers.Add("Location", url);
            return Ok(new { gameId });
        }

        [HttpGet("wait/{initCode}")]
        public async Task<ActionResult> WaitForVerification(string initCode)
        {
            var waitHandle = _initCodePolling.GetWaitHandle(initCode);
            var gameId = await waitHandle.WaitAsync();

            if (!gameId.HasValue)
                throw new TimeoutException();

            var url = Url.Action(nameof(GetState), new { gameId });
            Response.Headers.Add("Location", url);
            return Ok(new { gameId });
        }

        [HttpGet("state/{gameId:Guid}")]
        public ActionResult GetState(Guid gameId)
        {
            var score = _quest.GetState(gameId);
            return Ok(score);
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