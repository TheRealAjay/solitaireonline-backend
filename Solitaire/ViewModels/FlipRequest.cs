using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class FlipRequest
    {
        [Required] public string Position { get; set; } = null!;
        [Required] public int SolitaireSessionId { get; set; }
        [Required] public bool ManualFlip { get; set; }
    }
}