using MediatR;

using Microsoft.Extensions.Options;

using yor_auth_api.Features.Login.Models;
using yor_auth_api.Features.Specifications;
using yor_auth_api.Infrastructure.Contracts;
using yor_auth_api.Infrastructure.Exceptions;
using yor_auth_api.Infrastructure.Helpers;
using yor_auth_api.Model;

namespace yor_auth_api.Features.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtSettings _jwtSettings;

        public LoginCommandHandler(
            IAuthRepository authRepository,
            ILogger<LoginCommandHandler> logger,
            IOptions<JwtSettings> options)
        {
            _authRepository =  authRepository 
                ?? throw new ArgumentNullException(nameof(authRepository));

            _jwtSettings = options is not null ? options.Value 
                : throw new ArgumentNullException(nameof(options));
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.Single( 
                new UserByEmailAndPasswordSpecification(request),
                cancellationToken, null);

            if(user is null)
            {
                throw new UserNotFoundException($"User with email: {request.Login} is not found");
            }

            var token = new LoginResponse
            {
                Access_Token = JwtHelper.GenerateJwtToken(user, _jwtSettings)
            };

            return token;
        }
    }
}
