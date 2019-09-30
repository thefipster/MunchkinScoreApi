using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.GameApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        public IActionResult Index() => Content("Running...");

        [Route("api/home/isAuthenticated")]
        public IActionResult IsAuthenticated() => new ObjectResult(User.Identity.IsAuthenticated);

        [Route("api/home/fail")]
        public IActionResult Fail() => Unauthorized();

        [Route("/home/[action]")]
        public IActionResult Denied() => Content("You need to allow this application access in Google+ to be able to login");
    }
}