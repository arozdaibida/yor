using System.Security.Claims;

using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api.Application.Service.Service
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IRepository<User> _repository;

        public CurrentUserService(IRepository<User> repository)
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
