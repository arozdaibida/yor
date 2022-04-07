using yor_database_infrastructure.UnitOfWork;

using yor_request_api.Infrastructure.Contexts;

namespace yor_request_api.Infrastructure.RequestUnitOfWork
{
    public class RequestUnitOfWork : UnitOfWork, IRequestUnitOfWork
    {
        public RequestUnitOfWork(DatabaseContext context)
            : base(context) { }
    }
}
