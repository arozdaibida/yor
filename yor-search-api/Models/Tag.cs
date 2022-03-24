using yor_database_infrastructure.Models;

namespace yor_search_api.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
