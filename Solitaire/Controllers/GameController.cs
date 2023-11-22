using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;
using Solitaire.ViewModels;
using System;

namespace Solitaire.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
            Card[] cards = new Card[52];

            CreateDrawpile(gameRequest, cards);
            CreateColumnsAndRows(gameRequest, cards);

            return Ok(cards);
        }

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

                card.Postition = "draw" + (i + 1);
                card.Flipped = false;
                card.SolitaireSessionId = gameRequest.SolitaireSessionId;
                
                cards[i] = card;
            }
        }

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
                    Suit = (Suit)random.Next(0, 4),
                    Rank = (Rank)random.Next(0, 13)
                };

            } while (cardsToCheck.Any(c => c.Suit == card.Suit && c.Rank == card.Rank));

            return card;
        }

        private void GenerateGameDeckCards(int startValue, int endValue, int column, Card[] cards, GameRequest gameRequest)
        {
            Card card;
            for (int i = startValue; i <= endValue; i++)
            {
                card = GetValidCard(cards);

                card.Postition = $"c{column}r{i - (i -1)}";
                card.Flipped = i != 24;
                card.SolitaireSessionId = gameRequest.SolitaireSessionId;

                cards[i] = card;
            }
        }
    }
}
