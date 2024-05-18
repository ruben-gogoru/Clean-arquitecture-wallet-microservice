using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;
using wallet_microservice_dotnet._1.Domain.RepositoryInterfaces;

namespace wallet_microservice_dotnet._3.Infraestructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> CreateAsync(T newEntity)
        {
            bool created = false;

            try
            {
                var entryEntity = await _unitOfWork.Context.Set<T>().AddAsync(newEntity);
                return entryEntity.Entity;
            }
            catch(Exception ex)
            {
                throw;
            }

            
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> match)
        {
            if(match != null)
                return await _unitOfWork.Context.Set<T>().FirstOrDefaultAsync<T>(match);

            throw new Exception("Null predicate: " + match.ToString());
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
    }
}
