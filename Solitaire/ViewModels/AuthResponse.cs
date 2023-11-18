namespace Solitaire.ViewModels
{
    public class AuthResponse
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Base64String { get; set; } = "";
    }
}
