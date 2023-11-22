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
        Heart,  // Herz
        Diamond, // Raute
        Spade,  // Pic
        Club   // Kreuz
    }

    // Wert
    public enum Rank
    {
        Rank_A,
        RANK_2,
        RANK_3,
        RANK_4,
        RANK_5,
        RANK_6,
        RANK_7,
        RANK_8,
        RANK_9,
        RANK_10,
        RANK_J,
        RANK_Q,
        RANK_K,
    }
}
