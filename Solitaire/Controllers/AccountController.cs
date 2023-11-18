using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solitaire.DataAccess.Models;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.DataAccess.Services.IServices;
using Solitaire.ViewModels;

namespace Solitaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody]AuthRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Modelstate not valid.");

                var user = await _userManager.FindByNameAsync(model.UserName)
                    ?? throw new UnauthorizedAccessException($"Username or password wrong");

                var isValidPwd = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isValidPwd)
                    throw new UnauthorizedAccessException("Username or password wrong!");

                var accessToken = _tokenService.CreateToken(user);
                await _unitOfWork.SaveAsync();

                return Ok(new AuthResponse
                {
                    Username = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Base64String = "data:image/svg+xml;base64," + user.Base64Picture,
                    Token = accessToken,
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return NotFound();
        }
    }
}
