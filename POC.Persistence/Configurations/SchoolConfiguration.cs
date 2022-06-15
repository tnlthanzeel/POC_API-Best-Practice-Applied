using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.Domain.Entitities;

namespace POC.Persistence.Configurations
{
    public class SchoolMasterConfiguration : IEntityTypeConfiguration<SchoolMaster>
    {
        public void Configure(EntityTypeBuilder<SchoolMaster> builder)
        {
            builder.Property(e => e.Location)
                          .IsRequired()
                          .HasMaxLength(100);

            builder.Property(e => e.Name)
                          .IsRequired()
                          .HasMaxLength(100);


            //SeedData(builder);
        }


        private void SeedData(EntityTypeBuilder<SchoolMaster> builder)
        {
            for (int i = 1; i <= 1000; i++)
            {

                builder.HasData(new SchoolMaster
                {
                    Id = Guid.NewGuid(),
                    Name = $"{i}-Zahira College",
                    Location = $"{i}-College",
                    NumberOfEmployees = 1000,
                    NumberOfStudents = 1000
                });
            }
        }
    }
}
