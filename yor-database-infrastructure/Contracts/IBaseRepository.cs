using System.Linq.Expressions;

using yor_database_infrastructure.Models;
using yor_database_infrastructure.Specification;

namespace yor_database_infrastructure.Contracts
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        IQueryable<T> Get(ISpecification<T> filter, params Expression<Func<T, object>>[] includes);

        Task<T> Single(ISpecification<T> filter, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes);

        void Delete(T body);

        void Update(T body);

        Task Insert(T body, CancellationToken cancellationToken);
    }
}
