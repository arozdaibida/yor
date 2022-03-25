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
                x.Tags.Any(y => tags.Contains(y));

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
