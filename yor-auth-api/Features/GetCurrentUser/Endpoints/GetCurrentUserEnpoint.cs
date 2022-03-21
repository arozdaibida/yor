using FastEndpoints;

using MediatR;

using yor_auth_api.Features.GetCurrentUser.Models;
using yor_auth_api.Features.GetCurrentUser.Queries;

namespace yor_auth_api.Features.GetCurrentUser.Endpoints
{
    public class GetCurrentUserEnpoint : EndpointWithoutRequest<GetCurrentUserResponse>
    {
        private readonly ISender _mediator;

        public GetCurrentUserEnpoint(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("api/auth/me");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = User.Claims.FirstOrDefault(x => 
                x.Type == Constants.JwtToken.Claims.UserIdClaim).Value;

            if (userId is null)
            {
                await SendNotFoundAsync();
            }

            var user = await _mediator.Send(
                new GetCurrentUserQuery 
                { 
                    UserId = new Guid(userId) 
                });

            await SendOkAsync(user);
        }
    }
}
