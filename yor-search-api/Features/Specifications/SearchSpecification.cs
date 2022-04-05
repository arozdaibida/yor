using System.Linq.Expressions;

using yor_database_infrastructure.Specification;

using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class SearchSpecification : BaseSpecification<User>
    {
        public SearchSpecification(
            IEnumerable<Tag> tags,
            string gender,
            string country,
            string city,
            int minAge,
            int maxAge)
        {
            Select = x =>
                (tags.Count() == 0 || x.Tags.Any(y => tags.Contains(y)))
                && (string.IsNullOrEmpty(gender) || x.Gender == gender)
                && (string.IsNullOrEmpty(country) || x.Country == country)
                && (string.IsNullOrEmpty(city) || x.City == city)
                && (minAge <= 16 || GetAge(x.DateOfBirth) >= minAge)
                && (maxAge <= 16 || GetAge(x.DateOfBirth) >= maxAge);

            Joins = new List<Expression<Func<User, object>>>
            {
                x => x.Tags
            };
        }

        private static int GetAge(DateTime date)
        {
            var today = DateTime.Today;

            var now = (today.Year * 100 + today.Month) * 100 + today.Day;
            var then = (date.Year * 100 + date.Month) * 100 + date.Day;

            return (now - then) / 10000;
        }
    }
}
