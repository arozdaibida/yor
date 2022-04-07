using MediatR;

using yor_request_api.Application.Contracts;
using yor_request_api.Application.Exceptions;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class CreateRequestCommandHandler : AsyncRequestHandler<CreateRequestCommand>
    {
        private readonly IRequestUnitOfWork _unitOfWork;
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<User> _userRepository;

        public CreateRequestCommandHandler(
            IRequestUnitOfWork unitOfWork,
            IRepository<Request> requestRepository,
            IRepository<User> userRepository)
        {
            _requestRepository = requestRepository
                ?? throw new ArgumentNullException(nameof(requestRepository));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));

            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        protected override async Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            await SelectUserById(request.SenderId, cancellationToken);

            await SelectUserById(request.RecipientId, cancellationToken);

            var body = new Request
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                CreatedBy = Constants.AppName,
                CreatedAt = DateTime.UtcNow,
            };

            await _requestRepository.Insert(body, cancellationToken);
            await _unitOfWork.Commit();
        }

        private async Task<User> SelectUserById(Guid userId, CancellationToken token)
        {
            return await _userRepository.Single(new UserByIdSpecification(userId), token)
                ?? throw new UserNotFoundException($"There is no user with id: {userId}");
        }
    }
}
