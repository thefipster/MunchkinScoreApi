using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameStorage;

namespace TheFipster.Munchkin.GameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly IMonsterStore _monsterStore;

        public MonsterController(IMonsterStore monsterStore)
        {
            _monsterStore = monsterStore;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var monsters = _monsterStore.Get();
            return Ok(monsters);
        }

        [Authorize]
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var monster = _monsterStore.Get(name);
            return Ok(monster);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Monster monster)
        {
            var updatedMonster = _monsterStore.Upsert(monster);
            var url = Url.Action(nameof(GetByName), new { name = updatedMonster.Name });
            return Created(url, updatedMonster);
        }
    }
}