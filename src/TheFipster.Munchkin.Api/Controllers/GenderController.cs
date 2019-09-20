using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain.Basics;

namespace TheFipster.Munchkin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult Get() => Ok(Genders.Items);
    }
}