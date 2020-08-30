using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
    {
        public void Configure(EntityTypeBuilder<StateProvince> builder)
        {
            builder.ToTable("Person.StateProvince");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .HasColumnName("StateProvinceID");

            builder.Property(sp => sp.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}