using System.ComponentModel.DataAnnotations;

namespace Solitaire.Models
{
    public class SolitaireSession
    {
        [Key]
        public int Id { get; set; }
        public DateTime? SessionStartDate { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Draw> Draws { get; set; }
    }
}
