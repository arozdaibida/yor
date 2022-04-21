using System.Linq.Expressions;

using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
{
    public class RequestByIdSpecification: BaseSpecification<Request>
    {
        public RequestByIdSpecification(Guid requestId)
        {
            Select = x => x.Id == requestId;

            Take = 1;

            Joins = new List<Expression<Func<Request, object>>>
            {
                x => x.Sender,
                x => x.Recipient
            };
        }
    }
}
