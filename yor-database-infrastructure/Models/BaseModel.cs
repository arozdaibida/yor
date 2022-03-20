namespace yor_database_infrastructure.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}
