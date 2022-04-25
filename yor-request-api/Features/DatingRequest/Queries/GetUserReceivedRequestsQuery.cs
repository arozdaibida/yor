using MediatR;
using yor_request_api.Application.Contracts;
using yor_request_api.Features.DatingRequest.Models;

namespace yor_request_api.Features.DatingRequest.Queries
{
    public class GetUserReceivedRequestsQuery : IRequest<IEnumerable<RequestResponse>>
    {
        public Guid UserId { get; set; }
    }
}
