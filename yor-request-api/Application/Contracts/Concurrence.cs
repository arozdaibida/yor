using yor_database_infrastructure.Models;

namespace yor_request_api.Application.Contracts
{
    public class Concurrence : BaseModel
    {
        public Guid SenderId { get; set; }

        public Guid RecipientId { get; set; }

        public User Sender { get; set; }

        public User Recipient { get; set; }

        public string State { get; set; }
    }
}
