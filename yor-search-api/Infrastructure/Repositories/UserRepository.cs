using yor_database_infrastructure.Repositories;

using yor_search_api.Infrastructure.Contexts;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
            : base(context) { }
    }
}
