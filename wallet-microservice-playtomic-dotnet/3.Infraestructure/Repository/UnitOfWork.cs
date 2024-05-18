using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using wallet_microservice_dotnet._1.Domain.DatabaseContext;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;

namespace wallet_microservice_dotnet._3.Infraestructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContextData Context { get; }

        public UnitOfWork(DbContextData contextData)
        {
            Context = contextData;
        }

        public void Commit<T>()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
