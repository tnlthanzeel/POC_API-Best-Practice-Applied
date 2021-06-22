using Microsoft.EntityFrameworkCore;
using POC.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Persistence
{
    public class POCDbContext : DbContext
    {
        public POCDbContext(DbContextOptions<POCDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<SchoolMaster> School { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(POCDbContext).Assembly);
        }

    }
}
