using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TheFipster.Munchkin.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => new JsonResult(
            from c in User.Claims select new { c.Type, c.Value });

        [AllowAnonymous]
        [HttpGet("authenticated")]
        public IActionResult IsAuthenticated() => Ok(User.Identity.IsAuthenticated);
    }
}