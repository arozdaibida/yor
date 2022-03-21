using MediatR;
using System.Security.Claims;
using yor_auth_api.Features.Specifications;
using yor_auth_api.Infrastructure.Contracts;
using yor_auth_api.Model;

namespace yor_auth_api.Features.Registration.Commands
{
    public class RegistrationCommandHandler : AsyncRequestHandler<RegistrationCommand>
    {
        private readonly IAuthUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;

        public RegistrationCommandHandler(
            IAuthUnitOfWork unitOfWork,
            IAuthRepository authRepository)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));

            _authRepository = authRepository
                ?? throw new ArgumentNullException(nameof(authRepository));
        }

        protected override async Task Handle(
            RegistrationCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await _authRepository.Single(
                new UserByEmailSpecification(request.Email), 
                cancellationToken);

            if(user is not null)
            {
                throw new ArgumentException($"User with email: {request.Email} is already being used");
            }

            user = new User
            {
                Id = Guid.NewGuid(),
                DateofBirth = request.DateOfBirth,
                City = request.City,
                Country = request.Country,
                CreatedAt = DateTime.Now,
                CreatedBy = Constants.AppName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Password = request.Password,
            };

            await _authRepository.Insert(user, cancellationToken);
            await _unitOfWork.Commit();
        }
    }
}
