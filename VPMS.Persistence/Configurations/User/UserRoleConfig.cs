using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Persistence.Configurations.User;

internal class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
                new UserRole
                {
                    Id = Guid.Parse("e24f4cd1-0759-440e-9a2b-6072880392b6"),
                    ConcurrencyStamp = "d6b0ba4f-67d0-4a6d-b37d-60f891d0875d",
                    Name = "superadmin",
                    NormalizedName = "superadmin".ToUpper()
                }
            );
    }
}
