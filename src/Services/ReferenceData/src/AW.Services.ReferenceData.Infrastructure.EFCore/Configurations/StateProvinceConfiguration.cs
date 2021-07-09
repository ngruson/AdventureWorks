using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
    {
        public void Configure(EntityTypeBuilder<StateProvince> builder)
        {
            builder.ToTable("StateProvince");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .HasColumnName("StateProvinceID");

            builder.Property(sp => sp.StateProvinceCode)
                .IsRequired();
                //.HasMaxLength(3);

            builder.Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(sp => sp.CountryRegion)
                .WithMany(c => c.StateProvinces)
                .HasForeignKey(sp => sp.CountryRegionCode);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}