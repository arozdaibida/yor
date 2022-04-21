using MediatR;

using yor_request_api.Application.Contracts;
using yor_request_api.Application.Exceptions;
using yor_request_api.Features.Specifications;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

namespace yor_request_api.Features.DatingConcurrence.Commands
{
    public class DeleteConcurrenceCommandHandler : AsyncRequestHandler<DeleteConcurrenceCommand>
    {
        private readonly IRepository<Concurrence> _concurrenceRepository;
        private readonly IRequestUnitOfWork _unitOfWork;

        public DeleteConcurrenceCommandHandler(
            IRepository<Concurrence> concurrenceRepository, 
            IRequestUnitOfWork unitOfWork)
        {
            _concurrenceRepository = concurrenceRepository
                ?? throw new ArgumentNullException(nameof(concurrenceRepository));
            
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(DeleteConcurrenceCommand request, CancellationToken cancellationToken)
        {
            var query = new ConcurrenceByIdSpecification(request.ConcurrenceId);
            var concurrency = await _concurrenceRepository.Single(query, cancellationToken)
                ?? throw new ConcurrenceNotFoundException($"There is no concurrence with id: {request.ConcurrenceId}");

            _concurrenceRepository.Delete(concurrency);
            await _unitOfWork.Commit();
        }
    }
}
