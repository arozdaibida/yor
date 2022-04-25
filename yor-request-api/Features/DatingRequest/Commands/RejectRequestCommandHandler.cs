using MediatR;
using yor_request_api.Application.Contracts;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class RejectRequestCommandHandler : AsyncRequestHandler<RejectRequestCommand>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Concurrence> _concurrenceRepository;
        private readonly IRequestUnitOfWork _unitOfWork;

        public RejectRequestCommandHandler(
            IRepository<Request> reuqestRepository, 
            IRepository<Concurrence> concurrenceRepository, 
            IRequestUnitOfWork unitOfWork)
        {
            _requestRepository = reuqestRepository
                ?? throw new ArgumentNullException(nameof(reuqestRepository));

            _concurrenceRepository = concurrenceRepository
                ?? throw new ArgumentNullException(nameof(concurrenceRepository));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RejectRequestCommand request, CancellationToken cancellationToken)
        {
            var requestByIdQuery = new RequestByIdSpecification(request.RequestId);
            var rejectedRequest = await _requestRepository.Single(
                requestByIdQuery,
                cancellationToken)
                ?? throw new ArgumentNullException($"There is no request with id: {request.RequestId}");
            
            _requestRepository.Delete(rejectedRequest);

            var concurrence = new Concurrence
            {
                Id = Guid.NewGuid(),
                SenderId = rejectedRequest.SenderId,
                Sender = rejectedRequest.Sender,
                RecipientId = rejectedRequest.RecipientId,
                Recipient = rejectedRequest.Recipient,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Constants.AppName,
                State = Constants.State.Rejected
            };
            await _concurrenceRepository.Insert(concurrence, cancellationToken);

            await _unitOfWork.Commit();
        }
    }
}
