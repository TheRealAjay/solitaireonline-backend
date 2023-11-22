using System.ComponentModel.DataAnnotations;

namespace Solitaire.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public string Postition { get; set; } = null!;
        public bool Flipped { get; set; }

        public virtual int SolitaireSessionId { get; set; }
        public virtual SolitaireSession SolitaireSession { get; set; }
    }

    public enum Suit
    {
        Heart,   // Herz
        Diamond, // Raute
        Spade,   // Pic
        Clover   // Kreuz
    }

    // Wert
    public enum Rank
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
