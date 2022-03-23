using MediatR;

using System.Security.Claims;

using yor_auth_api.Features.GetCurrentUser.Models;

namespace yor_auth_api.Features.GetCurrentUser.Queries
{
    public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
    {
        public ClaimsPrincipal Claims { get; set; }
    }
}
