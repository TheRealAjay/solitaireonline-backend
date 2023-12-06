using System.ComponentModel.DataAnnotations;

namespace Solitaire.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public CardType Type { get; set; }
        public Value Value { get; set; }
        public string Position { get; set; } = null!;
        public bool Flipped { get; set; }

        public virtual int SolitaireSessionId { get; set; }
        public virtual SolitaireSession SolitaireSession { get; set; }
    }

    public enum CardType
    {
        Heart,   // Herz
        Diamond, // Raute
        Spade,   // Pic
        Clover   // Kreuz
    }

    // Wert
    public enum Value
    {
        Rank_A,
        Rank_2,
        Rank_3,
        Rank_4,
        Rank_5,
        Rank_6,
        Rank_7,
        Rank_8,
        Rank_9,
        Rank_10,
        Rank_J,
        Rank_Q,
        Rank_K,
    }
}
