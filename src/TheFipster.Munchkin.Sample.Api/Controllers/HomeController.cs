using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Sample.Api
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index() => Content("Sample API at your service.");
    }
}