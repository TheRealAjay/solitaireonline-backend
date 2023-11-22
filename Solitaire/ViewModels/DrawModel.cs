using Solitaire.Models;
using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class DrawModel
    {
        public int Id { get; set; }
        public int Sort { get; set; }
        public string FromPosition { get; set; } = null!;
        public string ToPosition { get; set; } = null!;

        public virtual SolitaireSession SolitaireSession { get; set; }
    }
}
