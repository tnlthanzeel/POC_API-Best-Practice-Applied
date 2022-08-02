using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPMS.Domain.Entities.IdentityUserEntities;

namespace VPMS.Persistence.Configurations.User;

internal class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var user = new ApplicationUser()
        {
            Id = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5"),
            UserName = "admin",
            Email = "admin@vpms.com",
            LockoutEnabled = false,
            PhoneNumber = "1234567890",
            CreatedOn = new DateTimeOffset(new DateTime(2022, 6, 30)),
            NormalizedEmail = "admin@vpms.com".ToUpper(),
            NormalizedUserName = "admin".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = "70428f75-0a6f-4d92-a2cd-ae4e0cdbd10f",
            ConcurrencyStamp = "70428f75-0a6f-4d92-a2cd-ae4e0cdbd10f"
        };

        user.PasswordHash = "AQAAAAEAACcQAAAAEJZHh/S5hmTm+8BR8ssy2GyMm04koddmCJLLGetMIWDEwKTXVwjow5mnIKwK5ExMNA==";
        builder.HasData(user);
    }
}
