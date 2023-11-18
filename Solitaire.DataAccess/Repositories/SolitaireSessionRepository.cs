using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Models;
using Solitaire.DataAccess.Repositories.IRepositories;

namespace Solitaire.DataAccess.Repositories
{
    public class SolitaireSessionRepository : Repository<SolitaireSession>, ISolitaireSessionRepository
    {
        public SolitaireSessionRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
