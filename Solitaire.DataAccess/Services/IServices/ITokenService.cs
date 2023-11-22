using Solitaire.Models;

namespace Solitaire.DataAccess.Services.IServices
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
