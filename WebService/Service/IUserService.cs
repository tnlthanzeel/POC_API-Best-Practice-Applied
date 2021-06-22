using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;

namespace WebService.Service
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
    }
}
