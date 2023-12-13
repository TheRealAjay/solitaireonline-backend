using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;

namespace Solitaire.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        public ISolitaireSessionRepository SolitaireSessions { get; }

        public ICardRepository Cards { get; }

        public IDrawRepository Draws { get; }

        public IScoreRepository Scores { get; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SolitaireSessions = new SolitaireSessionRepository(context);
            Cards = new CardRepository(context);
            Draws = new DrawRepository(context);
            Scores = new ScoreRepository(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
