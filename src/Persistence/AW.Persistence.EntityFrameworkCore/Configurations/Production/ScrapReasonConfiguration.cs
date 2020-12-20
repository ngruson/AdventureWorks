using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ScrapReasonConfiguration : IEntityTypeConfiguration<ScrapReason>
    {
        public void Configure(EntityTypeBuilder<ScrapReason> builder)
        {
            builder.ToTable("Production.ScrapReason");

            builder.Property(sr => sr.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}