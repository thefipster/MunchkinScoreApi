using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.Api.Controllers
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

        [Route("api/me")]
        [Authorize]
        public IActionResult Me()
        {
            var name = User.FindFirst(ClaimTypes.GivenName).Value;
            var email = User.FindFirst(ClaimTypes.Email).Value;
            return Ok(new { name, email });
        }

        [Route("/home/[action]")]
        public IActionResult Denied() => Content("You need to allow this application access in Google+ to be able to login");
    }
}