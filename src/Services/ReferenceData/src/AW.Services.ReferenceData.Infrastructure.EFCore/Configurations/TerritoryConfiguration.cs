using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
    {
        public void Configure(EntityTypeBuilder<Territory> builder)
        {
            builder.ToTable("Territory");
            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("TerritoryID");

            builder.Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.Group)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}