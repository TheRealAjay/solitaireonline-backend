using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
