using System.Security.Claims;

using yor_auth_api.Model;

namespace yor_auth_api.Application.Services
{
    public interface IAuthService
    {
        Task<User> GetCurrentUser(ClaimsPrincipal claims, CancellationToken cancellationToken);
    }
}
