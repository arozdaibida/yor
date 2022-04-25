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
        private readonly IRepository<Concurrence> _concurrenceRepository;

        public CreateRequestCommandHandler(
            IRequestUnitOfWork unitOfWork,
            IRepository<Request> requestRepository,
            IRepository<User> userRepository,
            IRepository<Concurrence> concurrenceRepository)
        {
            _requestRepository = requestRepository
                ?? throw new ArgumentNullException(nameof(requestRepository));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));

            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));

            _concurrenceRepository = concurrenceRepository
                ?? throw new ArgumentNullException(nameof(concurrenceRepository));
        }

        protected override async Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            await SelectUserById(request.SenderId, cancellationToken);
            await SelectUserById(request.RecipientId, cancellationToken);

            if(await IsRequestOrConcurrenceExist(request.SenderId, request.RecipientId, cancellationToken))
            {
                return;
            }

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
            var query = new UserByIdSpecification(userId);
            return await _userRepository.Single(query, token)
                ?? throw new UserNotFoundException($"There is no user with id: {userId}");
        }

        private async Task<bool> IsRequestOrConcurrenceExist(
            Guid senderId, 
            Guid recipientId, 
            CancellationToken cancellationToken)
        {
            var concurrecnceQuery = new ConcurrenceByUserIdSpecification(senderId, recipientId);
            var isConcurrenceExist = await _concurrenceRepository.Single(
                concurrecnceQuery, 
                cancellationToken) is not null;

            var requestQuery = new RequestByUserIdSpecification(senderId, recipientId);
            var isRequestExist = await _requestRepository.Single(
                requestQuery, 
                cancellationToken) is not null;

            return isConcurrenceExist && isRequestExist;
        }
    }
}
