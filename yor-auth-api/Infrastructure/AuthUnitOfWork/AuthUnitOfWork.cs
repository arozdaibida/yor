using yor_auth_api.Infrastructure.Contexts;
using yor_auth_api.Infrastructure.Contracts;

using yor_database_infrastructure.UnitOfWork;

namespace yor_auth_api.Infrastructure.AuthUnitOfWork
{
    public class AuthUnitOfWork : UnitOfWork, IAuthUnitOfWork
    {
        public AuthUnitOfWork(DatabaseContext context)
            : base(context) { }
    }
}
