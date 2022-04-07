using System.Linq.Expressions;

using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
{
    public class RequestsBySenderIdSpecification : BaseSpecification<Request>
    {
        public RequestsBySenderIdSpecification(Guid senderId)
        {
            Select = x => x.SenderId == senderId;

            OrderByDesc = x => x.CreatedBy;

            Joins = new List<Expression<Func<Request, object>>>
            {
                x => x.Recipient,
                x => x.Sender
            };
        }
    }
}
