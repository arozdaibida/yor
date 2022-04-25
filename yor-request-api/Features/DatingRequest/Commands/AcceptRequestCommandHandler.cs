using MediatR;

using yor_request_api.Application.Contracts;
using yor_request_api.Application.Exceptions;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class AcceptRequestCommandHandler : AsyncRequestHandler<AcceptRequestCommand>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Concurrence> _concurrenceRepository;
        private readonly IRequestUnitOfWork _unitOfWork;

        public AcceptRequestCommandHandler(
            IRepository<Request> requestRepository,
            IRepository<Concurrence> concurrenceRepository,
            IRequestUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository
                ?? throw new ArgumentNullException(nameof(requestRepository));

            _concurrenceRepository = concurrenceRepository
                ?? throw new ArgumentNullException(nameof(concurrenceRepository));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        protected override async Task Handle(AcceptRequestCommand request, CancellationToken cancellationToken)
        {
            var query = new RequestByIdSpecification(request.RequestId);
            var acceptedRequest = await _requestRepository.Single(
                query,
                cancellationToken)
                ?? throw new RequestNotFoundException($"There is no request with id: {request.RequestId}");
            
            _requestRepository.Delete(acceptedRequest);

            var concurrence = new Concurrence
            {
                Id = Guid.NewGuid(),
                SenderId = acceptedRequest.SenderId,
                Sender = acceptedRequest.Sender,
                RecipientId = acceptedRequest.RecipientId,
                Recipient = acceptedRequest.Recipient,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Constants.AppName,
                State = Constants.State.Accepted
            };
            await _concurrenceRepository.Insert(concurrence, cancellationToken);

            await _unitOfWork.Commit();
        }
    }
}
