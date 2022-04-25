using MediatR;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class CreateRequestCommand : IRequest
    {
        public Guid SenderId { get; set; }

        public Guid RecipientId { get; set; }
    }
}
