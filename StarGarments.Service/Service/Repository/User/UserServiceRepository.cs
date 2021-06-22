using StarGarments.Service.Service.Base.Interface;
using System;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.Base
{
    public class UserServiceRepository : IUserServiceRepository
    {
        public string CreateEndPoint { get; set; } = "users";
        public string UpdateEndPoint { get; set; } = "users/";
        public string GetEndPoint { get; set; } = "users";
        public string DeleteEndPoint { get; set; } = "users/";

        private HttpServiceRepository httpServiceRepository;

        public UserServiceRepository()
        {
            httpServiceRepository = new HttpServiceRepository();
        }

        public async Task Create<T>(T request)
        {
            await httpServiceRepository.Post<T>(CreateEndPoint, request);
        }

        public async Task Delete(Guid id)
        {
            await httpServiceRepository.Delete(DeleteEndPoint + id);
        }

        public async Task<T> Get<T>()
        {
            return await httpServiceRepository.Get<T>(GetEndPoint);
        }

        public async Task<T> GetById<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update<T>(Guid id, T request)
        {
            await httpServiceRepository.Put<T>(UpdateEndPoint + id, request);
        }
    }
}
