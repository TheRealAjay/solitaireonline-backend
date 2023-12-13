namespace Solitaire.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ISolitaireSessionRepository SolitaireSessions { get; }
        ICardRepository Cards { get; }
        IDrawRepository Draws { get; }
        IScoreRepository Scores { get; }
        Task SaveAsync();
    }
}
