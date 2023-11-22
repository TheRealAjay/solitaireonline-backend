using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;

namespace Solitaire.DataAccess.Repositories
{
    public class SolitaireSessionRepository : Repository<SolitaireSession>, ISolitaireSessionRepository
    {
        public SolitaireSessionRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
