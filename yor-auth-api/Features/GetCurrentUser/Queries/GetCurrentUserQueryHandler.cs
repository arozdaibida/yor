using MediatR;

using yor_auth_api.Application.Services;
using yor_auth_api.Features.GetCurrentUser.Models;

namespace yor_auth_api.Features.GetCurrentUser.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
    {
        private readonly IAuthService _authService;

        public GetCurrentUserQueryHandler(IAuthService authService)
        {
            _authService = authService
                ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _authService.GetCurrentUser(request.Claims, cancellationToken);

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
