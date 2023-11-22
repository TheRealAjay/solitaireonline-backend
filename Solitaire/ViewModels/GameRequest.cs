using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class GameRequest
    {
        [Required]
        public int SolitaireSessionId { get; set; }
    }
}
