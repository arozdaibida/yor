using yor_search_api.Models;

namespace yor_search_api.Features.Search.Models
{
    public class SearchResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public ICollection<TagResponse> Tags { get; set; }

        public class TagResponse
        {
            public Guid Id { get; set; }

            public string Name { get; set;}
        }

        public static SearchResponse Map(User user)
            => new SearchResponse
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Age = GetAge(user.DateOfBirth),
                Country = user.Country,
                City = user.City,
                Email = user.Email,
                Gender = user.Gender,
                Tags = user.Tags.Select(x =>
                    new TagResponse
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
            };

        private static int GetAge(DateTime date)
        {
            var today = DateTime.Today;

            var now = (today.Year * 100 + today.Month) * 100 + today.Day;
            var then = (date.Year * 100 + date.Month) * 100 + date.Day;

            return (now - then) / 10000;
        }
    }
}
