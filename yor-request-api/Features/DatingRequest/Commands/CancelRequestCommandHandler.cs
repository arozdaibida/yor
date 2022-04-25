using MediatR;

using yor_request_api.Application.Contracts;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class CancelRequestCommandHandler : AsyncRequestHandler<CancelRequestCommand>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRequestUnitOfWork _unitOfWork;

        public CancelRequestCommandHandler(
            IRepository<Request> requestRepository,
            IRequestUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository
                ?? throw new ArgumentNullException(nameof(requestRepository));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CancelRequestCommand request, CancellationToken cancellationToken)
        {
            var query = new RequestByIdSpecification(request.RequestId);
            var deletedRequest = await _requestRepository.Single(query, cancellationToken);
            
            _requestRepository.Delete(deletedRequest);
            
            await _unitOfWork.Commit();
        }
    }
}
