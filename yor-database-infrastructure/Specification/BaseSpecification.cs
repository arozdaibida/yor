using System.Linq.Expressions;

using yor_database_infrastructure.Models;

namespace yor_database_infrastructure.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Select { get; set ; }

        public IEnumerable<Expression<Func<T, object>>> Joins { get; set; }
        
        public Expression<Func<T, object>> OrderBy { get; set; }
        
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        
        public int Take { get; set; }
        
        public int Skip { get; set; }
        
        public bool IsPagingEnable { get; set; }

        public void And(ISpecification<T> specification)
        {
            var binaryExpression = Expression.AndAlso(Select.Body, specification.Select.Body);
            ParameterExpression[] parameters = new ParameterExpression[1]
            {
                Expression.Parameter(typeof(T), Select.Parameters.First().Name)
            };

            Select = Expression.Lambda<Func<T, bool>>(binaryExpression, parameters);
        }
    }
}
