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
    public class SessionController : ControllerBase
    {
        #region PRIVATE FIELDS
        private readonly ILogger<SessionController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public SessionController(
            ILogger<SessionController> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        #region PUBLIC ACTIONS
        /// <summary>
        /// Gets a session
        /// If there is no session created it will be returned Ok(null)
        /// </summary>
        /// <returns>
        ///     Ok(session) if there is a session
        ///     Ok(null) if there is no session
        /// </returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetSession()
        {
            try
            {
                var user = await GetUser();

                var session = await _unitOfWork.SolitaireSessions.GetFirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

                var score = (await _unitOfWork.Scores.GetAllAsync())
                    .SingleOrDefault(c => c.ApplicationUserId == user.Id && !c.IsFinished);

                if (session is null || score is null)
                    return Ok(null);

                session.SessionContinuedOn = DateTime.UtcNow;

                await _unitOfWork.SolitaireSessions.UpdateAsync(session);
                await _unitOfWork.SaveAsync();

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

        /// <summary>
        /// Creates a new session
        /// If an existing session is already in use it will be deleted
        /// </summary>
        /// <returns> Ok(session) if the session was [deleted] and created successfully </returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> StartSession()
        {
            try
            {
                ApplicationUser user = await GetUser();
                SolitaireSession? session = await _unitOfWork.SolitaireSessions.GetFirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

                if (session is not null)
                {
                    var scoreToDelete = await _unitOfWork.Scores.GetSingleAsync(c => c.ApplicationUserId == user.Id && !c.IsFinished);

                    await _unitOfWork.SolitaireSessions.RemoveAsync(session);
                    await _unitOfWork.Scores.RemoveAsync(scoreToDelete);
                    await _unitOfWork.SaveAsync();
                }

                var utcNow = DateTime.UtcNow;
                session = new()
                {
                    ApplicationUserId = user.Id,
                    SessionStartDate = utcNow,
                    SessionContinuedOn = utcNow,
                };

                Score newScore = new()
                {
                    ApplicationUserId = user.Id,
                    ScoreCount = 0,
                    GameDuration = new TimeSpan(0, 0, 0, 0, 0, 0),
                    IsFinished = false
                };

                await _unitOfWork.Scores.AddAsync(newScore);
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

        /// <summary>
        /// Pauses the timer for the session
        /// </summary>
        /// <param name="gameRequest"> GameRequest stores the solitaire session id </param>
        /// <returns> StatusCode 200 if it was successful </returns>
        [HttpPost]
        [Route("pause")]
        public async Task<IActionResult> PauseSession([FromBody] GameRequest gameRequest)
        {
            try
            {
                SolitaireSession session = await _unitOfWork.SolitaireSessions.FindAsync(gameRequest.SolitaireSessionId)
                    ?? throw new KeyNotFoundException("No session crated.");
                Score score = await _unitOfWork.Scores.GetSingleAsync(c => c.ApplicationUserId == session.ApplicationUserId && !c.IsFinished);
                
                if (session is not null && score is not null)
                {
                    score.GameDuration += DateTime.UtcNow.Subtract(session.SessionContinuedOn.Value);
                    await _unitOfWork.Scores.UpdateAsync(score);
                    await _unitOfWork.SaveAsync();
                }

                return Ok();
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

        /// <summary>
        /// Deletes the session, cards, draws and set the isFinished flag in the score table to true
        /// </summary>
        /// <param name="gameRequest"> GameRequest stores the solitaire session id </param>
        /// <returns> StatusCode 200 if it was successful </returns>
        [HttpPost]
        [Route("end")]
        public async Task<IActionResult> EndSession([FromBody] GameRequest gameRequest)
        {
            try
            {
                SolitaireSession session = await _unitOfWork.SolitaireSessions.FindAsync(gameRequest.SolitaireSessionId)
                    ?? throw new KeyNotFoundException("No session created.");
                Score score = await _unitOfWork.Scores.GetSingleAsync(c => c.ApplicationUserId == session.ApplicationUserId && !c.IsFinished);
                var draws = (await _unitOfWork.Draws.GetAllAsync())
                    .Where(c => c.SolitaireSessionId == gameRequest.SolitaireSessionId);
                var cards = (await _unitOfWork.Cards.GetAllAsync())
                    .Where(c => c.SolitaireSessionId == gameRequest.SolitaireSessionId);

                score.IsFinished = true;
                if (session is not null && score is not null)
                {
                    score.GameDuration += DateTime.UtcNow.Subtract(session.SessionContinuedOn.Value);
                    await _unitOfWork.Scores.UpdateAsync(score);
                }

                await _unitOfWork.SolitaireSessions.RemoveAsync(session);
                await _unitOfWork.SaveAsync();

                return Ok();
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

        #region PRIVATE FUNCTIONS
        private async Task<ApplicationUser> GetUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name)
                ?? throw new KeyNotFoundException("User not found.");

            return user;
        }
        #endregion
    }
}
