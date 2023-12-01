using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Helpers;
using Solitaire.Models;
using Solitaire.ViewModels;

namespace Solitaire.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GameController(
            ILogger<GameController> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("initialize")]
        public async Task<IActionResult> Initialize([FromBody]GameRequest gameRequest)
        {
            try
            {
                Card[] cards = new Card[52];

                CreateDrawpile(gameRequest, cards);
                CreateColumnsAndRows(gameRequest, cards);

                await _unitOfWork.Cards.AddRangeAsync(cards);
                await _unitOfWork.SaveAsync();

                return Ok(cards);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("move")]
        public async Task<IActionResult> Move(DrawRequest drawRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Modelstate not valid.");

                var draws = (await _unitOfWork.Draws.GetAllAsync())
                    .Where(c => c.SolitaireSessionId == drawRequest.SolitaireSessionId);
                var cards = (await _unitOfWork.Cards.GetAllAsync())
                    .Where(c => c.SolitaireSessionId == drawRequest.SolitaireSessionId);

                var card = cards.Single(c => c.Postition == drawRequest.FromPosition);
                var toPositionChar = drawRequest.ToPosition.ToArray();
                List<string> toPosition = new()
                {
                    toPositionChar[0].ToString()
                };

                ParseCharArrayToList(toPositionChar, toPosition);

                switch (toPosition[0])
                {
                    case "c":
                        // Moved to card deck cxrx
                        if (await CheckCardForColumn(toPosition, card))
                            return Ok(true);

                        return Ok(false);

                    case "b":
                        // Move to build
                        if (await CheckCardForBuild(toPosition, card))
                            return Ok(true);

                        return Ok(false);

                    default:
                        throw new Exception("Postion not available");
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region PRIVATE FUNCTIONS

        /// <summary>
        /// Creates the drawpile
        /// </summary>
        /// <param name="gameRequest"> The gamerequest with the SolitaireSessionId </param>
        /// <param name="cards"> The cards array which should be munipulated </param>
        private void CreateDrawpile(GameRequest gameRequest, Card[] cards)
        {
            for (int i = 0; i < 24; i++)
            {
                Card card = GetValidCard(cards);

                card.Postition = "d" + (i + 1);
                card.Flipped = false;
                card.SolitaireSessionId = gameRequest.SolitaireSessionId;
                
                cards[i] = card;
            }
        }

        /// <summary>
        /// Generates the column and row cards
        /// </summary>
        /// <param name="gameRequest"> Stores the solitaireSessionID </param>
        /// <param name="cards"> The cards array </param>
        private void CreateColumnsAndRows(GameRequest gameRequest, Card[] cards)
        {
            var card = GetValidCard(cards);
            card.Postition = "c1r1";
            card.Flipped = true;
            card.SolitaireSessionId = gameRequest.SolitaireSessionId;
            cards[24] = card;

            GenerateGameDeckCards(25, 26, 2, cards, gameRequest);
            GenerateGameDeckCards(27, 29, 3, cards, gameRequest);
            GenerateGameDeckCards(30, 33, 4, cards, gameRequest);
            GenerateGameDeckCards(34, 38, 5, cards, gameRequest);
            GenerateGameDeckCards(39, 44, 6, cards, gameRequest);
            GenerateGameDeckCards(45, 51, 7, cards, gameRequest);
        }

        /// <summary>
        /// Creates a valid card
        /// A card is valid if the cards dont hold a card with the suit and rank
        /// </summary>
        /// <param name="cards"> The cards array which contains the cards </param>
        /// <returns> A valid card </returns>
        private Card GetValidCard(Card[] cards)
        {
            Random random = new();
            Card card;
            var cardsToCheck = cards.Where(c => c is not null);

            do
            {
                card = new Card()
                {
                    Type = (CardType)random.Next(0, 4),
                    Value = (Value)random.Next(0, 13)
                };

            } while (cardsToCheck.Any(c => c.Type == card.Type && c.Value == card.Value));

            return card;
        }

        /// <summary>
        /// Generates the cxrx cards
        /// </summary>
        /// <param name="startValue"> The startvalue of the row </param>
        /// <param name="endValue"> The endvalue of the row </param>
        /// <param name="column"> The column the card lays </param>
        /// <param name="cards"> The cards array </param>
        /// <param name="gameRequest"> Stores the solitairesessionID </param>
        private void GenerateGameDeckCards(int startValue, int endValue, int column, Card[] cards, GameRequest gameRequest)
        {
            Card card;
            for (int i = startValue; i <= endValue; i++)
            {
                card = GetValidCard(cards);

                // Generates the position cxrx begining with cxr1
                card.Postition = $"c{column}r{i - (startValue - 1)}";
                // flipped true --> value and type visible
                card.Flipped = i == endValue;
                card.SolitaireSessionId = gameRequest.SolitaireSessionId;

                cards[i] = card;
            }
        }

        private void ParseCharArrayToList(char[] toPositionChar, List<string> toPosition)
        {
            string tempPos = string.Empty;

            for (int i = 1; i < toPositionChar.Length; i++)
            {
                if (int.TryParse(toPositionChar[i].ToString(), out int _))
                {
                    tempPos += toPositionChar[i].ToString();
                }
                else
                {
                    toPosition.Add(tempPos);
                    tempPos = string.Empty;
                }
            }

            toPosition.Add(tempPos);
        }

        private async Task<bool> CheckCardForColumn(List<string> toPosition, Card card)
        {
            // cxrx --> toPosition has 4 entries
            GetPositionAsInt(toPosition[1].ToString(), out int column);
            GetPositionAsInt(toPosition[3].ToString(), out int row);

            await CheckIfCardExists($"c{column}r{row}");

            if (row == 0)
                return true;

            var parent = await _unitOfWork.Cards.GetFirstOrDefaultAsync(c => c.Postition == $"c{column}r{row - 1}")
                ?? throw new ArgumentException("Parent card does not exist.");

            if (CardHelper.CanBePlacedBottom(parent, card))
                return true;

            return false;
        }

        private async Task<bool> CheckCardForBuild(List<string> toPosition, Card card)
        {
            // bx --> toPosition has two entries
            GetPositionAsInt(toPosition[1].ToString(), out int buildPosition);

            await CheckIfCardExists($"d{buildPosition}");

            if (buildPosition == 0)
                return true;

            var parent = await _unitOfWork.Cards.GetFirstOrDefaultAsync(c => c.Postition == $"d{buildPosition - 1}")
                ?? throw new ArgumentException("Parent card does not exist.");

            if (CardHelper.CanBePlacedOnBuild(parent, card))
                return true;

            return false;
        }

        private void GetPositionAsInt(string number, out int position)
        {
            if (!int.TryParse(number, out position))
                throw new ArgumentException("Number has the wrong format.");
        }

        private async Task CheckIfCardExists(string position)
        {

            var existingCard = await _unitOfWork.Cards.GetFirstOrDefaultAsync(c => c.Postition == position);
            if (existingCard is not null)
                throw new ArgumentException("Card exists.");
        }

        #endregion
    }
}
