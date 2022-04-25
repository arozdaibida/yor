using yor_auth_api.Infrastructure.Contracts;

using yor_auth_api.Model;

using System.Security.Claims;

using yor_auth_api.Features.Specifications;

namespace yor_auth_api.Application.Services.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthServices(IAuthRepository repository)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal claims, CancellationToken cancellationToken)
        {
            var userId = new Guid(claims.Claims.FirstOrDefault(x =>
                x.Type == Constants.JwtToken.Claims.UserIdClaim).Value);

            var user = _repository.Single(new UserByIdSpecification(userId), cancellationToken);

            return user;
        }
    }
}
