using Solitaire.Models;
using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class DrawModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public string FromPosition { get; set; } = null!;
        [Required]
        public string ToPosition { get; set; } = null!;

        [Required]
        public int SolitaireSessionId { get; set; }
        public SolitaireSession? SolitaireSession { get; set; }
    }
}
