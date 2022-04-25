using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_request_api.Application.Contracts;
using yor_request_api.Application.Exceptions;
using yor_request_api.Features.DatingConcurrence.Models;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;

namespace yor_request_api.Features.DatingConcurrence.Queries
{
    public class GetUserConcurrencesQueryHandler : IRequestHandler<GetUserConcurrencesQuery, IEnumerable<ConcurrenceResponse>>
    {
        private readonly IRepository<Concurrence> _concurrenceRepository;
        private readonly IRepository<User> _userRepository;

        public GetUserConcurrencesQueryHandler(
            IRepository<Concurrence> concurrenceRepository, 
            IRepository<User> userRepository)
        {
            _concurrenceRepository = concurrenceRepository
                ?? throw new ArgumentNullException(nameof(concurrenceRepository));

            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<ConcurrenceResponse>> Handle(GetUserConcurrencesQuery request, CancellationToken cancellationToken)
        {
            var userQuery = new UserByIdSpecification(request.UserId);
            var user = await _userRepository.Single(userQuery, cancellationToken)
                ?? throw new UserNotFoundException($"There is no user with id: {request.UserId}");
            
            var concurreceQuery = new ConcurrencesByUserIdSpecification(request.UserId);
            var concurrences = _concurrenceRepository
                .Get(concurreceQuery)
                .Select(x => ConcurrenceResponse.Map(x, request.UserId));

            return await concurrences.ToListAsync(cancellationToken);
        }
    }
}
