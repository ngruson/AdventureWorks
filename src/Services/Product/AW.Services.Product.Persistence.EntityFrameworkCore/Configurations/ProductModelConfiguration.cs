using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductModelConfiguration : IEntityTypeConfiguration<Domain.ProductModel>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductModel> builder)
        {
            builder.ToTable("ProductModel");

            builder.Property(pmc => pmc.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pmc => pmc.CatalogDescription)
                .HasColumnType("xml");

            builder.Property(pmc => pmc.Instructions)
                .HasColumnType("xml");
        }
    }
}