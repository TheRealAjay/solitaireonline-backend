using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string PasswordConfirm { get; set; } = null!;
    }
}
