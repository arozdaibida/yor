using System.Linq.Expressions;

using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
{
    public class ConcurrenceByIdSpecification: BaseSpecification<Concurrence>
    {
        public ConcurrenceByIdSpecification(Guid id)
        {
            Select = x => x.Id == id;

            Take = 1;

            Joins = new List<Expression<Func<Concurrence, object>>>
            {
                x => x.Sender,
                x => x.Recipient
            };
        }
    }
}
