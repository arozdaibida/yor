using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.DatingRequest.Models
{
    public class RequestResponse
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public UserResponse Sender { get; set; }

        public Guid RecipientId { get; set; }

        public UserResponse Recipient { get; set; }

        public DateTime CreatedAt { get; set; }

        public static RequestResponse Map(Request request)
            => new RequestResponse
            {
                Id = request.Id,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                CreatedAt = request.CreatedAt,
                Sender = UserResponse.Map(request.Sender),
                Recipient = UserResponse.Map(request.Recipient),
            };
    }
}
