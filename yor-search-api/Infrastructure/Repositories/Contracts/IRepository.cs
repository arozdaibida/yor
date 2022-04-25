using yor_database_infrastructure.Contracts;
using yor_database_infrastructure.Models;

namespace yor_search_api.Infrastructure.Repositories.Contracts
{
    public interface IRepository<T> : IBaseRepository<T> where T : BaseModel
    {
    }
}
