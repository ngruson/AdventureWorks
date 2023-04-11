using AW.Services.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class CultureConfiguration : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> builder)
        {
            builder.ToTable("Culture");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("CultureID");
        }
    }
}
