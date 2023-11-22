using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;

namespace Solitaire.DataAccess.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
