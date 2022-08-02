using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPMS.Domain.Permissions;

namespace VPMS.Persistence.Configurations.User;

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.HasData(new IdentityUserClaim<Guid>()
        {
            Id = 1,
            ClaimType = CustomClaimTypes.Permission,
            ClaimValue = SuperUserPermissions.All,
            UserId = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5")
        });
    }
}
