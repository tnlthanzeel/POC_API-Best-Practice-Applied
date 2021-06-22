using StarGarments.Service.Service.Base;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.Repository.OperationBreakdown.Interface
{
    public interface IOperationBreakdownRepository : IApplicationServiceRepository
    {
        Task<T> GetStyles<T>();
        Task<T> GetStyleById<T>();
    }
}
