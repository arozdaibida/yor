using MediatR;

namespace yor_auth_api.Features.Registration.Commands
{
    public class RegistrationCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
