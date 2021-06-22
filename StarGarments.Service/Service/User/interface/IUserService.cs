using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.User
{
    public interface IUserService
    {
        IEnumerable<Stargarments.Domain.Entities.User> Users { get; }

        Task<List<Stargarments.Domain.Entities.User>> LoadUsersAsync();
        Task UpdateUsersAsync(Stargarments.Domain.Entities.User user);
        Task SaveUserAsync(Stargarments.Domain.Entities.User user);
        Task DeleteUserAsync(Guid id);
    }
}
