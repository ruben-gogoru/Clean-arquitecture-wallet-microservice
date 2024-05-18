using System.Linq.Expressions;

namespace wallet_microservice_dotnet._1.Domain.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includeProperties = "");
        Task<T> CreateAsync(T entity);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
    }
}
