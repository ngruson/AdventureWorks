using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class IllustrationConfiguration : IEntityTypeConfiguration<Illustration>
    {
        public void Configure(EntityTypeBuilder<Illustration> builder)
        {
            builder.ToTable("Production.Illustration");

            builder.Property(i => i.Diagram)
                .HasColumnType("xml");
        }
    }
}