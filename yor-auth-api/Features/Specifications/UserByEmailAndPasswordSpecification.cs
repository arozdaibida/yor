using yor_auth_api.Model;

using yor_database_infrastructure.Specification;

namespace yor_auth_api.Features.Specifications
{
    public class UserByEmailAndPasswordSpecification : BaseSpecification<User>
    {
        public UserByEmailAndPasswordSpecification(
            string login,
            string password)
        {
            Select = x
                => x.Email == login
                && x.Password == password;

            Take = 1;
        }
    }
}
