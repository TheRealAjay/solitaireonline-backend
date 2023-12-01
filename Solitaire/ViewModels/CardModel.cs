using Solitaire.Models;

namespace Solitaire.ViewModels
{
    public class CardModel
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public Value Value { get; set; }
        public string Postition { get; set; } = null!;
        public bool Flipped { get; set; }

        public int SolitaireSessionId { get; set; }
        public SolitaireSession SolitaireSession { get; set; }
    }
}
