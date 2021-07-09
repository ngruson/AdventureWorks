using AW.Services.Product.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductProductPhotoConfiguration : EntityTypeConfiguration<ProductProductPhoto>
    {
        public ProductProductPhotoConfiguration()
        {
            ToTable("Production.ProductProductPhoto");
            HasKey(ppp => new { ppp.ProductId, ppp.ProductPhotoId });

            Property(ppp => ppp.ProductId)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(ppp => ppp.ProductPhotoId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}