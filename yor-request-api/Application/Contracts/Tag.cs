using yor_database_infrastructure.Models;

namespace yor_request_api.Application.Contracts
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
