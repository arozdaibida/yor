using FastEndpoints;

using MediatR;

using yor_auth_api.Features.Registration.Commands;
using yor_auth_api.Features.Registration.Models;

namespace yor_auth_api.Features.Registration.Endpoints
{
    public class RegistrationEndpoint : Endpoint<RegistrationRequest>
    {
        private readonly ISender _mediator;

        public RegistrationEndpoint(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("api/auth/registration");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegistrationRequest req, CancellationToken ct)
        {
            try
            {
                await _mediator.Send(new RegistrationCommand
                {
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    City = req.City,
                    Country = req.Country,
                    Email = req.Email,
                    Gender = req.Gender,
                    Password = req.Password,
                    DateOfBirth = req.DateOfBirth,
                });

                await SendOkAsync();
            }
            catch (ArgumentException)
            {
                await SendErrorsAsync();
            }
        }
    }
}
