using System;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.Base
{
    public interface IApplicationServiceRepository
    {
        Task<T> Get<T>();
        Task<T> GetById<T>(Guid id);
        Task Create<T>(T request);
        Task Update<T>(Guid id,T request);
        Task Delete(Guid request);
    }
}
