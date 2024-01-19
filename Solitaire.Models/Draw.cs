using System.ComponentModel.DataAnnotations;

namespace Solitaire.Models
{
    public class Draw
    {
        [Key] public int Id { get; set; }
        public int Sort { get; set; }
        public string FromPosition { get; set; } = null!;
        public string ToPosition { get; set; } = null!;
        public bool WasFlipped { get; set; }
        public bool WasManualFlip { get; set; }
        public bool FlippedAllOnDrawDeck { get; set; }

        public virtual int SolitaireSessionId { get; set; }
        public virtual SolitaireSession SolitaireSession { get; set; }
    }
}