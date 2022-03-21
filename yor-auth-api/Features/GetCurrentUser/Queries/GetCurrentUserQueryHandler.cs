using MediatR;

using yor_auth_api.Features.GetCurrentUser.Models;
using yor_auth_api.Features.Specifications;
using yor_auth_api.Infrastructure.Contracts;

namespace yor_auth_api.Features.GetCurrentUser.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
    {
        private readonly IAuthRepository _authRepository;

        public GetCurrentUserQueryHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository
                ?? throw new ArgumentNullException(nameof(authRepository));
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await  _authRepository.Single(
                new UserByIdSpecification(request.UserId), cancellationToken);

            return new GetCurrentUserResponse
            {
                Id = user.Id,
                City = user.City,
                Country = user.Country,
                DateOfBirth = user.DateofBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender
            };
        }
    }
}
