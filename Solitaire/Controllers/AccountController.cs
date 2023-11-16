using Microsoft.AspNetCore.Mvc;

namespace Solitaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate()
        {
            return NotFound();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return NotFound();
        }
    }
}
