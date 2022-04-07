using yor_database_infrastructure.Specification;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Features.Specifications
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
