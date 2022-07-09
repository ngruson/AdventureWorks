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
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("StateProvinceID");

            builder.Property(_ => _.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(_ => _.CountryRegion)
                .WithMany(_ => _.StatesProvinces)
                .HasForeignKey("CountryRegionCode");
            
                
            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}