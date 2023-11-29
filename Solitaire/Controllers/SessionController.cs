using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;

namespace Solitaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public SessionController(
            ILogger<SessionController> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetSession()
        {
            try
            {
                var user = await GetUser();

                var session = await _unitOfWork.SolitaireSessions.GetFirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

                if (session is null)
                    return Ok(null);

                session.ApplicationUser = null;

                return Ok(session);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Authorize]
        [Route("create")]
        public async Task<IActionResult> StartSession()
        {
            try
            {
                var user = await GetUser();

                var session = await _unitOfWork.SolitaireSessions.GetFirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

                if (session is not null)
                {
                    await _unitOfWork.SolitaireSessions.RemoveAsync(session);
                    await _unitOfWork.SaveAsync();
                }

                session = new SolitaireSession()
                {
                    ApplicationUserId = user.Id,
                    SessionStartDate = DateTime.UtcNow
                };

                await _unitOfWork.SolitaireSessions.AddAsync(session);
                await _unitOfWork.SaveAsync();

                return Ok(session);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        private async Task<ApplicationUser> GetUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name)
                ?? throw new KeyNotFoundException("User not found.");

            return user;
        }
    }
}
