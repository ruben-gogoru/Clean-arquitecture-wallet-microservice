using wallet_microservice_playtomic_dotnet._1.Domain.DatabaseContext;

namespace wallet_microservice_playtomic_dotnet._1.Domain.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContextData Context { get; }
        void Commit<T>();
    }
}
