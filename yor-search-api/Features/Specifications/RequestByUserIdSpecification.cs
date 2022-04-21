using yor_database_infrastructure.Specification;
using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class RequestByUserIdSpecification : BaseSpecification<Request>
    {
        public RequestByUserIdSpecification(Guid senderId, Guid recipientId)
        {
            Select = x => (x.SenderId == senderId) || (x.RecipientId == recipientId);

            Take = 1;
        }
    }
}
