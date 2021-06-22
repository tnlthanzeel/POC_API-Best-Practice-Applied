using Microsoft.EntityFrameworkCore;
using WebService.Model;

namespace WebService.Entity
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
