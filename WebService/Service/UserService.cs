using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Entity;
using WebService.Model;

namespace WebService.Service
{
    public class UserService : IUserService
    {
        private readonly EntityContext _context;
        public UserService(EntityContext context)
        {
            _context = context;
        }

        public async  Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }
    }
}
