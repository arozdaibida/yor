using yor_database_infrastructure.Specification;

using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class TagsByNamesSpecification : BaseSpecification<Tag>
    {
        public TagsByNamesSpecification(IEnumerable<string> tags)
        {
            Select = x => tags.Contains(x.Name);
        }
    }
}
