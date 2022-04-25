namespace yor_request_api.Features.DatingRequest.Models
{
    public class CreateRequestRequest
    {
        public Guid SenderId { get; set; }

        public Guid RecipientId { get; set; }
    }
}
