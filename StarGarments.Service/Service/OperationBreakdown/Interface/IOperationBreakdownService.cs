using Stargarments.Domain.Entities.OperationBreakDown;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.OperationBreakdown
{
    public interface IOperationBreakdownService
    {
        Task<List<GarmentTypeModel>> LoadGarmentTypesAsync();
        Task<List<StyleModel>> LoadStylesAsync();
        Task<SMVBreakDownVersion> GetStyleById();
    }
}
