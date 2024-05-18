using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using wallet_microservice_playtomic_dotnet._1.Domain.DatabaseContext;
using wallet_microservice_playtomic_dotnet._1.Domain.RepositoryInterfaces;

namespace wallet_microservice_playtomic_dotnet._3.Infraestructure
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
            using var transaction = Context.Database.BeginTransaction();
            SetIdentityInsert<T>(Context, enable: true);
            Context.SaveChanges();
            SetIdentityInsert<T>(Context, enable: false);
            transaction.Commit();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        private Task SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            return context.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
        }
    }
}
