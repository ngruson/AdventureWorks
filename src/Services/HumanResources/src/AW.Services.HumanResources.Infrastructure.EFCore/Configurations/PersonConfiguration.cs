using Microsoft.EntityFrameworkCore;

namespace AW.Services.HumanResources.Infrastructure.EFCore.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Core.Entities.Person>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("PersonID");

            builder.Property(_ => _.ObjectId)
                .HasDefaultValue();

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.FirstName).HasColumnName("FirstName");

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.MiddleName).HasColumnName("MiddleName");

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.LastName).HasColumnName("LastName");
        }
    }
}
