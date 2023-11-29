using Solitaire.Models;

namespace Solitaire.Helpers
{
    public static class CardHelper
    {
        /// <summary>
        /// Checks a card if it is black
        /// </summary>
        /// <param name="card"> The card which should be check if the color is black </param>
        /// <returns> True if the card is Spade or Clover otherwise false </returns>
        public static bool IsBlack(Card card) => card.Suit == Suit.Spade || card.Suit == Suit.Clover;

        /// <summary>
        /// Checks a card if it is red
        /// </summary>
        /// <param name="card"> The card which should be check if the color is red </param>
        /// <returns> True if the card is Heart or Diamond otherwise false </returns>
        public static bool IsRed(Card card) => card.Suit == Suit.Heart || card.Suit == Suit.Diamond;

        /// <summary>
        /// Checks if the colors are different
        /// </summary>
        /// <param name="first"> The first card </param>
        /// <param name="second"> The second card </param>
        /// <returns> True if the first card is Spade or Club and the second card is Heart or Diamond otherwise false </returns>
        /// <returns> True if the first card is Heart or Diamond and the second card is Spade or Club otherwise false </returns>
        public static bool IsAlternateColor(Card first, Card second) => IsBlack(first) && IsRed(second);

        /// <summary>
        /// Checks if the cards are in ascending order (Q -> K)
        /// </summary>
        /// <param name="higher"> The higher card </param>
        /// <param name="lower"> The lower card </param>
        /// <returns> True if the higher card is one over the lower card otherwise false </returns>
        public static bool IsInSequence(Card higher, Card lower) => higher.Rank == lower.Rank + 1;

        /// <summary>
        /// Checks if the lower card can be placed under the higher card
        /// Therefor its important that the color alternates and the card is in sequence
        /// </summary>
        /// <param name="higher"> The higher card </param>
        /// <param name="lower"> The lower card </param>
        /// <returns> True if the lower card can be placed under the higher card otherwise false </returns>
        public static bool CanBePlacedBottom(Card higher, Card lower) => IsAlternateColor(higher, lower) && IsInSequence(higher, lower);

        /// <summary>
        /// Checks if the cards have the same suit
        /// </summary>
        /// <param name="first"> The first card </param>
        /// <param name="second"> The second card </param>
        /// <returns> True if the first card and the second card have the same suit otherwise false </returns>
        public static bool IsSameSuit(Card first, Card second) => first.Suit == second.Suit;

        /// <summary>
        /// Checks if the card can be placed at the foundation
        /// Therefor its important to have the same suit and are in sequence
        /// </summary>
        /// <param name="higher"> The higher card </param>
        /// <param name="lower"> The lower card </param>
        /// <returns> True if the card can be placed at foundation otherwise false </returns>
        public static bool CanBePlacedOnBuild(Card higher, Card lower) => IsSameSuit(higher, lower) && IsInSequence(higher, lower);
    }
}
