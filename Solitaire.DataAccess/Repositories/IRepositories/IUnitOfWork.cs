namespace Solitaire.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ISolitaireSessionRepository SolitaireSessions { get; }
        Task SaveAsync();
    }
}
