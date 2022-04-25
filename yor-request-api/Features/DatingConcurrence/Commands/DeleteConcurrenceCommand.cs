using MediatR;

namespace yor_request_api.Features.DatingConcurrence.Commands
{
    public class DeleteConcurrenceCommand: IRequest
    {
        public Guid ConcurrenceId { get; set; }
    }
}
