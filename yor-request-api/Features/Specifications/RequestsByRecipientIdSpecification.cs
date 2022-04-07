using System.Linq.Expressions;

using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
{
    public class RequestsByRecipientIdSpecification: BaseSpecification<Request>
    {
        public RequestsByRecipientIdSpecification(Guid recipientId)
        {
            Select = x => x.RecipientId == recipientId;

            OrderByDesc = x => x.CreatedAt;

            Joins = new List<Expression<Func<Request, object>>>
            {
                x => x.Recipient,
                x => x.Sender
            };
        }
    }
}
