using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VPMS.Persistence.Configurations.User;

internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(
               new IdentityUserRole<Guid>
               {
                   RoleId = Guid.Parse("e24f4cd1-0759-440e-9a2b-6072880392b6"),
                   UserId = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5"),
               }
           );
    }
}
