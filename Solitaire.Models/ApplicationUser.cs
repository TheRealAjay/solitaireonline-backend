using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solitaire.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public string Base64Picture { get; set; } = "";

        [ForeignKey(nameof(Models.SolitaireSession))]
        public int? SolitaireSessionId { get; set; }
        public SolitaireSession? SolitaireSession { get; set; }
    }
}
