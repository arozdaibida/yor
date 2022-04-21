using yor_database_infrastructure.Specification;

using yor_search_api.Models;

namespace yor_search_api.Features.Specifications
{
    public class UserByIdSpecification : BaseSpecification<User>
    {
        public UserByIdSpecification(Guid userId)
        {
            Select = x => x.Id == userId;

            Take = 1;
        }
    }
}
