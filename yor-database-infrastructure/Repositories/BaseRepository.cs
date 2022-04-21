using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

using yor_database_infrastructure.Contracts;
using yor_database_infrastructure.Models;
using yor_database_infrastructure.Specification;

namespace yor_database_infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly DbSet<T> _set;

        public BaseRepository(DbContext context)
        {
            _set = context is not null ? context.Set<T>() 
                : throw new ArgumentNullException(nameof(context));
        }

        public async Task Delete(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
        {
            var entity = await _set.FindAsync(filter, cancellationToken);

            _set.Remove(entity);
        }

        public void Delete(T body)
        {
            _set.Remove(body);
        }

        public IQueryable<T> Get(ISpecification<T> filter, params Expression<Func<T, object>>[] includes)
        {
            return ApplyFilter(filter);
        }

        public async Task Insert(T body, CancellationToken cancellationToken)
        {
            if(body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            await _set.AddAsync(body, cancellationToken);
        }

        public async Task<T> Single(ISpecification<T> filter, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            return await ApplyFilter(filter).FirstOrDefaultAsync();
        }

        public void Update(T body)
        {
            if(body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            _set.Update(body);
        }

        private IQueryable<T> ApplyFilter(ISpecification<T> filter)
        {
            return SpecificationEvaluator<T>.Process(_set.AsQueryable(), filter);
        }
    }
}
