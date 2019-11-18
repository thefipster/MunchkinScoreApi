using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.CardStash.Api.Controllers
{

    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index() => Content("Stash API at your service.");
    }
}