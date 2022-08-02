using Microsoft.AspNetCore.Identity;
using VPMS.SharedKernel.Interfaces;

namespace VPMS.Domain.Entities.IdentityUserEntities;

public class ApplicationUser : IdentityUser<Guid>, ICreatedAudit
{
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

    public string? CreatedBy { get; set; }
}
