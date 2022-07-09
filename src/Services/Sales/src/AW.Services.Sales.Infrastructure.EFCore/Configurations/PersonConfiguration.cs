using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("PersonID");

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.FirstName).HasColumnName("FirstName");

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.MiddleName).HasColumnName("MiddleName");

            builder.OwnsOne(_ => _.Name)
                .Property(_ => _.LastName).HasColumnName("LastName");
        }
    }
}