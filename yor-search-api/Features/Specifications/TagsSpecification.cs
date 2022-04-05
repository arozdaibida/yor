using yor_database_infrastructure.Specification;

using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class TagsSpecification : BaseSpecification<Tag>
    {
        public TagsSpecification()
        {
            Select = x => true;
        }
    }
}
