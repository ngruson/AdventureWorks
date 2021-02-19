using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class IllustrationConfiguration : IEntityTypeConfiguration<Domain.Illustration>
    {
        public void Configure(EntityTypeBuilder<Domain.Illustration> builder)
        {
            builder.ToTable("Illustration");

            builder.Property(i => i.Diagram)
                .HasColumnType("xml");
        }
    }
}