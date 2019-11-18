using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Monitoring.Api
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index() => Content("Monitoring API at your service.");
    }
}