using yor_database_infrastructure.Repositories;

using yor_search_api.Infrastructure.Contexts;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api.Infrastructure.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DatabaseContext context)
            : base(context) { }
    }
}
