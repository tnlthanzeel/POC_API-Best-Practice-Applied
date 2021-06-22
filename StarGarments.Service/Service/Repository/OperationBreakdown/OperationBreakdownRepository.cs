using StarGarments.Service.Service.Base;
using StarGarments.Service.Service.Repository.OperationBreakdown.Interface;
using System;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.Repository.OperationBreakdown
{
    public class OperationBreakdownRepository : IOperationBreakdownRepository
    {
        private HttpServiceRepository httpServiceRepository;
        public string GetGarmentTypesEndPoint { get; set; } = "ie/garment/types";
        public string GetStylesEndPoint { get; set; } = "ie/garment/style";
        public string GetStylesByIdEndPoint { get; set; } = "ie/garment/smvbreakdown/hht";
       

        public OperationBreakdownRepository()
        {
            httpServiceRepository = new HttpServiceRepository();
        }

        public Task Create<T>(T request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid request)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get<T>()
        {
            return await httpServiceRepository.Get<T>(GetGarmentTypesEndPoint);
        }

        public Task<T> GetById<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(Guid id, T request)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetStyles<T>()
        {
            return await httpServiceRepository.Get<T>(GetStylesEndPoint);
        }

        public async Task<T> GetStyleById<T>()
        {
            return await httpServiceRepository.Get<T>(GetStylesByIdEndPoint);
        }
    }
}
