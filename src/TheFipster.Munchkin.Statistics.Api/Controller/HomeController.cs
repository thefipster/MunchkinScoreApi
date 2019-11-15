using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Statistics.Api
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index() => Content("Statistics API at your service.");
    }
}