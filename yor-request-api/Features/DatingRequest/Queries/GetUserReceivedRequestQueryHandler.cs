using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_request_api.Application.Contracts;
using yor_request_api.Application.Exceptions;
using yor_request_api.Features.DatingRequest.Models;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;

namespace yor_request_api.Features.DatingRequest.Queries
{
    public class GetUserReceivedRequestQueryHandler : IRequestHandler<GetUserReceivedRequestsQuery, IEnumerable<RequestResponse>>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<User> _userRepository;

        public GetUserReceivedRequestQueryHandler(
            IRepository<Request> requestRepository,
            IRepository<User> userRepository)
        {
            _requestRepository = requestRepository
                ?? throw new ArgumentNullException(nameof(requestRepository));

            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<RequestResponse>> Handle(GetUserReceivedRequestsQuery request, CancellationToken cancellationToken)
        {
            var userByIdQuery = new UserByIdSpecification(request.UserId);
            var recipient = await _userRepository.Single(userByIdQuery, cancellationToken)
                ?? throw new UserNotFoundException($"There is no user with id: {request.UserId}");

            var requestsByRecipientIdQuery = new RequestsByRecipientIdSpecification(request.UserId);
            var requests = _requestRepository
                .Get(requestsByRecipientIdQuery)
                .Select(x => RequestResponse.Map(x));

            return await requests.ToListAsync(cancellationToken);
        }
    }
}
