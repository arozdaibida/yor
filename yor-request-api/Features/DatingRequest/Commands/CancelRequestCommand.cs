using MediatR;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class CancelRequestCommand: IRequest
    {
        public Guid RequestId { get; set; }
    }
}
