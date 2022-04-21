using System.Linq.Expressions;
using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
{
    public class ConcurrencesByUserIdSpecification: BaseSpecification<Concurrence>
    {
        public ConcurrencesByUserIdSpecification(Guid userId)
        {
            Select = x => (x.SenderId == userId) || (x.RecipientId == userId);

            Joins = new List<Expression<Func<Concurrence, object>>>
            {
                x => x.Sender,
                x => x.Recipient
            };
        }
    }
}
