using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Gaming.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        public IActionResult Index() => Content("Game API at your service.");
    }
}