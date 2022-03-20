using MediatR;

using yor_auth_api.Features.Login.Models;

namespace yor_auth_api.Features.Login.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
