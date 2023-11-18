using Solitaire.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.DataAccess.Services.IServices
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
