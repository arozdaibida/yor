namespace yor_auth_api.Model
{
    public class BaseEntity
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}
