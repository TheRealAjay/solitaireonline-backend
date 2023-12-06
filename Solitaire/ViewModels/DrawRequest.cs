using Solitaire.Models;
using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class DrawRequest
    {
        [Required]
        public string FromPosition { get; set; } = null!;
        [Required]
        public string ToPosition { get; set; } = null!;

        [Required]
        public int SolitaireSessionId { get; set; }
    }
}
