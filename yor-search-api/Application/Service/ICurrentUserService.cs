using System.Security.Claims;

using yor_search_api.Models;

namespace yor_search_api.Application.Service
{
    public interface ICurrentUserService
    {
        Task<User> GetCurrentUser(ClaimsPrincipal claims, CancellationToken cancellationToken);
    }
}
