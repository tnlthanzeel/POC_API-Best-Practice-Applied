using Microsoft.EntityFrameworkCore;
using POC.Domain.Entitities;
using static POC.Utility.BaseEnums;

namespace POC.Persistence;

public class POCDbContext : DbContext
{
    public POCDbContext(DbContextOptions<POCDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; } = null!;

    public DbSet<SchoolMaster> School { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(POCDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Gender>().HaveConversion<string>().HaveMaxLength(250);
    }

}
