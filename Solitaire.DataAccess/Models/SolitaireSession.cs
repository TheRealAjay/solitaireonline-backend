using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solitaire.DataAccess.Models
{
    public class SolitaireSession
    {
        [Key]
        public int Id { get; set; }
        public DateTime? SessionStartDate { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
