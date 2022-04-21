using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.DatingConcurrence.Models
{
    public class ConcurrenceResponse
    {
        public Guid Id { get; set; }

        public DateTime CreateAt { get; set; }

        public Guid UserId { get; set; }

        public UserResponse User { get; set; }

        public static ConcurrenceResponse Map(Concurrence concurrence, Guid userId)
            => new ConcurrenceResponse
            {
                Id = concurrence.Id,
                CreateAt = concurrence.CreatedAt,
                UserId = userId,
                User = concurrence.Sender.Id == userId ? 
                    UserResponse.Map(concurrence.Sender ): 
                    UserResponse.Map(concurrence.Recipient)
            };
    }
}
