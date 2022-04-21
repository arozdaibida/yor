using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.DatingConcurrence.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public static UserResponse Map(User user)
            => new UserResponse
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Age = GetAge(user.DateOfBirth),
                Country = user.Country,
                City = user.City,
                Email = user.Email,
                Gender = user.Gender
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
