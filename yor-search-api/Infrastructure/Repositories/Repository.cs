using yor_database_infrastructure.Models;
using yor_database_infrastructure.Repositories;

using yor_search_api.Infrastructure.Contexts;
using yor_search_api.Infrastructure.Repositories.Contracts;

namespace yor_search_api.Infrastructure.Repositories
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : BaseModel
    {
        public Repository(DatabaseContext context)
            : base(context) { }
    }
}
