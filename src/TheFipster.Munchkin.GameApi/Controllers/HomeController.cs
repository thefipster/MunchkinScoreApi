using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Munchkin.GameApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        public IActionResult Index() => Content("Running...");
    }
}