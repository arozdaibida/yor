using yor_auth_api.Model;
using yor_database_infrastructure.Contracts;

namespace yor_auth_api.Infrastructure.Contracts
{
    public interface IAuthRepository : IBaseRepository<User>
    {
    }
}
