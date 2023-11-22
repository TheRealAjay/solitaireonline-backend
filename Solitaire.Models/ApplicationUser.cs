using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solitaire.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public string Base64Picture { get; set; } = "";

        [ForeignKey(nameof(Models.SolitaireSession))]
        public virtual int? SolitaireSessionId { get; set; }
        public virtual SolitaireSession? SolitaireSession { get; set; }
    }
}
