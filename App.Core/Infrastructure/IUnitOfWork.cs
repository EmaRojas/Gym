namespace App.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        void BeginTransaction();
    }
}
