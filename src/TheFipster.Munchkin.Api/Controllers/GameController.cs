﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string GameIdHeaderName = "Munchkin-GameId";

        private readonly IQuest _quest;
        private readonly IInitializationCache _cache;
        private readonly IInitCodePollService _initCodePolling;
        private readonly IGameStatePollService _gameStatePolling;

        public GameController(
            IQuest quest, 
            IInitializationCache cache, 
            IInitCodePollService initCodePolling, 
            IGameStatePollService gameStatePolling)
        {
            _quest = quest;
            _cache = cache;
            _initCodePolling = initCodePolling;
            _gameStatePolling = gameStatePolling;
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

        [HttpGet("poll/{gameId:Guid}")]
        public async Task<ActionResult> GetStateAsync(Guid gameId)
        {
            var handle = _gameStatePolling.GetScoreRequest(gameId);
            var score = await handle.WaitAsync();

            if (score == null)
                throw new TimeoutException();

            return Ok(score);
        }

        [Authorize]
        [HttpPost("append")]
        public ActionResult AddMessage([FromBody] List<GameMessage> messages)
        {
            Scoreboard score = null;
            Guid gameId = getGameFromHeader();
            score = _quest.AddMessages(gameId, messages);
            _gameStatePolling.FinishRequest(gameId, score);
            return Ok(score);
        }

        private Guid getGameFromHeader()
        {
            var headers = Request.Headers[GameIdHeaderName];
            var firstHeader = headers.FirstOrDefault();

            if (firstHeader == null)
                throw new GameIdHeaderException($"Header {GameIdHeaderName} is missing.");

            if (Guid.TryParse(firstHeader, out var gameId))
                return gameId;

            throw new GameIdHeaderException($"Header {GameIdHeaderName} has invalid format. Guid4 is expected.");
        }
    }
}