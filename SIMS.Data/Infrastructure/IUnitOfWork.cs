namespace SIMS.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();

        void Dispose();
    }
}
