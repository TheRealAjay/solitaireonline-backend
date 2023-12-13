using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;
using Solitaire.ViewModels;

namespace Solitaire.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScoreController : ControllerBase
    {
        #region PRVIATE FIELDS
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public ScoreController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        #region PUBLIC ACTIONS
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetScoreForGame([FromBody] GameRequest gameRequest)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name)
                    ?? throw new KeyNotFoundException("No user found.");

                Score score = await _unitOfWork.Scores.GetSingleAsync(c => c.ApplicationUserId == user.Id && !c.IsFinished);
               
                score.ApplicationUser = null;
               
                return Ok(score);
            }
            catch (KeyNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllScores()
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name)
                    ?? throw new KeyNotFoundException("No user found.");

                IEnumerable<Score> scores = (await _unitOfWork.Scores.GetAllAsync())
                    .Where(c => c.ApplicationUserId == user.Id && c.IsFinished);

                return Ok(scores);
            }
            catch (KeyNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
