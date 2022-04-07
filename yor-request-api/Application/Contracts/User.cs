using yor_database_infrastructure.Models;

namespace yor_request_api.Application.Contracts
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public ICollection<Request> SentRequests { get; set; }

        public ICollection<Request> ReceivedRequests { get; set; }

        public ICollection<Concurrence> Concurrences { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
