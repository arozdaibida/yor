using Microsoft.EntityFrameworkCore;

using yor_database_infrastructure.Models;

namespace yor_database_infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : BaseModel
    {
        public static IQueryable<T> Process(IQueryable<T> query, ISpecification<T> filter)
        {
            if(filter.Select is not null)
            {
                query = query.Where(filter.Select);
            }

            if (filter.Joins is not null)
            {
                query = filter.Joins.Aggregate(query, (current, join) => current.Include(join));
            }

            if(filter.OrderBy is not null)
            {
                query = query.OrderBy(filter.OrderBy);
            }
            else if(filter.OrderByDesc is not null)
            {
                query = query.OrderByDescending(filter.OrderByDesc);
            }

            if (filter.IsPagingEnable)
            {
                query = query.Skip(filter.Skip).Take(filter.Take);
            }
            else if(filter.Take > 0)
            {
                query = query.Take(filter.Take);
            }

            return query;
        }
    }
}
