using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;

namespace Solitaire.DataAccess.Repositories
{
    public class DrawRepository : Repository<Draw>, IDrawRepository
    {
        public DrawRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
