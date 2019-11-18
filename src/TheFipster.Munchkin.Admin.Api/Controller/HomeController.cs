using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Admin.Api
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index() => Content("Admin API at your service.");
    }
}