using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Munchkin.StashDomain;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly IRead<Monster> _monsterReader;
        private readonly IWrite<Monster> _monsterWriter;
        private readonly ILogger<MonsterController> _logger;

        public MonsterController(IRead<Monster> monsterReader, IWrite<Monster> monsterSaver, ILogger<MonsterController> logger)
        {

            _monsterReader = monsterReader;
            _monsterWriter = monsterSaver;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Monster>> Get()
        {
            var monsters = _monsterReader.FindAll();
            return Ok(monsters);
        }

        [HttpGet("{name}")]
        public ActionResult<Monster> Get(string name)
        {
            var monster = _monsterReader.FindOne(name);

            if (monster == null)
                return NotFound();

            return Ok(monster);
        }

        [HttpPost]
        public IActionResult Post(Monster monster)
        {
            _monsterWriter.Write(monster);
            var url = Url.Action(nameof(Get), new { name = monster.Name });
            return Created(url, monster);
        }
    }
}