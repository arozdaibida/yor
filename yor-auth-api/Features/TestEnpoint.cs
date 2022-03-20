using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace yor_auth_api.Features
{
    public class TestEnpoint : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("api/auth/test");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendOkAsync();
        }
    }
}
