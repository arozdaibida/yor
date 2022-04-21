using MediatR;

using yor_request_api.Features.DatingConcurrence.Models;

namespace yor_request_api.Features.DatingConcurrence.Queries
{
    public class GetUserConcurrencesQuery: IRequest<IEnumerable<ConcurrenceResponse>>
    {
        public Guid UserId { get; set; }
    }
}
