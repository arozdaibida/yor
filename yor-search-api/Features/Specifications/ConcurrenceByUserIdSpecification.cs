using yor_database_infrastructure.Specification;

using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class ConcurrenceByUserIdSpecification : BaseSpecification<Concurrence>
    {
        public ConcurrenceByUserIdSpecification(Guid senderId, Guid recipientId)
        {
            Select = x => (x.RecipientId == senderId) || (x.SenderId == senderId);

            Take = 1;
        }
    }
}
