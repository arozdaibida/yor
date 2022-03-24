using yor_auth_api.Model;

using yor_database_infrastructure.Specification;

namespace yor_auth_api.Features.Specifications
{
    public class UserByEmailSpecification : BaseSpecification<User>
    {
        public UserByEmailSpecification(string email)
        {
            Select = x => x.Email == email;

            Take = 1;
        }
    }
}
