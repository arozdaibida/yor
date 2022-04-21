using MediatR;

namespace yor_request_api.Features.DatingRequest.Commands
{
    public class AcceptRequestCommand: IRequest
    {
        public Guid RequestId { get; set; }
    }
}
