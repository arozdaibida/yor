using yor_database_infrastructure.Models;

namespace yor_auth_api.Model
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}
