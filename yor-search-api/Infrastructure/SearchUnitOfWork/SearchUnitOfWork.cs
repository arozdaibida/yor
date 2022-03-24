using yor_database_infrastructure.UnitOfWork;

using yor_search_api.Infrastructure.Contexts;

namespace yor_search_api.Infrastructure.SearchUnitOfWork
{
    public class SearchUnitOfWork : UnitOfWork, ISearchUnitOfWork
    {
        public SearchUnitOfWork(DatabaseContext context)
            : base(context) { }
    }
}
