namespace yor_database_infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        Task Commit();

        void Dispose();
    }
}
