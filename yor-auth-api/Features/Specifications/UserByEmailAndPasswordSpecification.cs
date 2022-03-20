using yor_auth_api.Features.Login.Commands;
using yor_auth_api.Model;
using yor_database_infrastructure.Specification;

namespace yor_auth_api.Features.Specifications
{
    public class UserByEmailAndPasswordSpecification : BaseSpecification<User>
    {
        public UserByEmailAndPasswordSpecification(LoginCommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            Select = x
                => x.Email == command.Login
                && x.Password == command.Password;

            Take = 1;
        }
    }
}
