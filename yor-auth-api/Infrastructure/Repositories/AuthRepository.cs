using yor_auth_api.Infrastructure.Contexts;
using yor_auth_api.Infrastructure.Contracts;
using yor_auth_api.Model;

using yor_database_infrastructure.Repositories;

namespace yor_auth_api.Infrastructure.Repositories
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(DatabaseContext context) 
            : base(context) { }
    }
}
