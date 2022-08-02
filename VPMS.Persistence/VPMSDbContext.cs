using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VPMS.Domain.Entities.IdentityUserEntities;
using VPMS.Domain.Entities.TodoItemEntities;
using VPMS.SharedKernel.Interfaces;
using VPMS.SharedKernel.Models;

namespace VPMS.Persistence;

public sealed class VPMSDbContext : IdentityDbContext<ApplicationUser, UserRole, Guid>
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IDomainEventDispatcher? _dispatcher;

    public VPMSDbContext(DbContextOptions<VPMSDbContext> options, ILoggedInUserService loggedInUserService, IDomainEventDispatcher? dispatcher) : base(options)
    {
        _loggedInUserService = loggedInUserService;
        _dispatcher = dispatcher;

        SavingChanges += ModifyAuditInformation;
    }


    public DbSet<TodoItem> Todo => Set<TodoItem>();


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyCommanConfigurations();
        builder.ApplyConfigurationsFromAssembly(typeof(VPMSDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // configurationBuilder.Properties<Gender>().HaveConversion<string>().HaveMaxLength(250);
    }

    private void ModifyAuditInformation(object? sender, SavingChangesEventArgs e)
    {
        foreach (var entryEntities in ChangeTracker.Entries())
        {
            var entity = entryEntities.Entity;

            if (entity is ICreatedAudit createdEntry && entryEntities.State == EntityState.Added)
            {
                createdEntry.CreatedOn = DateTimeOffset.UtcNow;
                createdEntry.CreatedBy = _loggedInUserService.UserId;
            }

            else if (entity is IDeletedAudit deletedentry and { IsDeleted: true })
            {
                deletedentry.DeletedOn = DateTimeOffset.UtcNow;
                deletedentry.DeletedBy = _loggedInUserService.UserId;
            }

            else if (entity is IUpdatedAudit updatedEntry && entryEntities.State == EntityState.Modified)
            {
                updatedEntry.UpdatedOn = DateTimeOffset.UtcNow;
                updatedEntry.UpdatedBy = _loggedInUserService.UserId;
            }
        }
    }

}
