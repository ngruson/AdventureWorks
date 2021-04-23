using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Persistence.EntityFrameworkCore.Configurations
{
    public class TerritoryConfiguration : IEntityTypeConfiguration<Domain.Territory>
    {
        public void Configure(EntityTypeBuilder<Domain.Territory> builder)
        {
            builder.ToTable("Territory");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
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