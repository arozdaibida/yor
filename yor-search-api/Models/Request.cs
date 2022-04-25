using yor_database_infrastructure.Models;

namespace yor_search_api.Models
{
    public class Request : BaseModel
    {
        public Guid SenderId { get; set; }

        public Guid RecipientId { get; set; }

        public User Sender { get; set; }

        public User Recipient { get; set; }
    }
}
