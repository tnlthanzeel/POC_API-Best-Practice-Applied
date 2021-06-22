using StarGarments.Service.Service.Base;
using StarGarments.Service.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.User
{
    public class UserService : IUserService
    {
        private List<Stargarments.Domain.Entities.User> users = new List<Stargarments.Domain.Entities.User>();
        public IEnumerable<Stargarments.Domain.Entities.User> Users { get { return users; } }
        private UserServiceRepository userServiceRepository;

        public UserService()
        {
            userServiceRepository = new UserServiceRepository();
        }

        public async Task<List<Stargarments.Domain.Entities.User>> LoadUsersAsync()
        {
            var res = await userServiceRepository.Get<ReponseModel<List<Stargarments.Domain.Entities.User>>>();
            return users = res.Data;
        }

        public async Task UpdateUsersAsync(Stargarments.Domain.Entities.User user)
        {
            await userServiceRepository.Update<Stargarments.Domain.Entities.User>(user.Id,user);
        }

        public async Task SaveUserAsync(Stargarments.Domain.Entities.User user)
        {
            await userServiceRepository.Create<Stargarments.Domain.Entities.User>(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await userServiceRepository.Delete(id);
        }
    }
}
