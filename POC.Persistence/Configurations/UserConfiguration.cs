using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.Domain.Entitities;

namespace POC.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);


        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Address)
           .IsRequired()
           .HasMaxLength(900);

        builder.Property(e => e.School)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(e => e.Id)
         .IsRequired()
        .ValueGeneratedNever();

        //SeedData(builder);
    }


    private void SeedData(EntityTypeBuilder<User> builder)
    {
        for (int i = 1; i <= 10000; i++)
        {

            builder.HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = $"{i}-Umair",
                LastName = $"{i}-Salmaan",
                Address = $"{i}-Colombo",
                School = $"{i}-UCD Dublin",
                Gender = Utility.BaseEnums.Gender.Male
            });
        }
    }
}
