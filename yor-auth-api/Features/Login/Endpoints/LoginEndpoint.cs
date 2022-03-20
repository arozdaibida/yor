using FastEndpoints;
using MediatR;
using yor_auth_api.Features.Login.Commands;
using yor_auth_api.Features.Login.Models;

namespace yor_auth_api.Features.Login.Endpoints
{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        private ISender _mediator;

        public LoginEndpoint(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("api/auth/login");
            AllowAnonymous();
        }

        public async override Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var response = await _mediator.Send(
                new LoginCommand
                {
                    Login = req.Login,
                    Password = req.Password
                });

            await SendAsync(response);
        }
    }
}
