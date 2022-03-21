using MediatR;

using yor_auth_api.Features.GetCurrentUser.Models;

namespace yor_auth_api.Features.GetCurrentUser.Queries
{
    public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
