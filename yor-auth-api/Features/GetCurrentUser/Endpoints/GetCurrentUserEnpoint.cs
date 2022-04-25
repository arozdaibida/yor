using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var user = await _mediator.Send(
                new GetCurrentUserQuery 
                { 
                    Claims = User
                });

            if (user is null)
            {
                await SendUnauthorizedAsync();
            }

            await SendOkAsync(user);
        }
    }
}
