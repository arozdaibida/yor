using yor_auth_api.Model;

using yor_database_infrastructure.Specification;

namespace yor_auth_api.Features.Specifications
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
