namespace yor_search_api.Features.Models
{
    public class SearchRequest
    {
        public IEnumerable<string> Tags { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }
    }
}
