using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductReviewConfiguration : EntityTypeConfiguration<ProductReview>
    {
        public ProductReviewConfiguration()
        {
            ToTable("Production.ProductReview");

            Property(pr => pr.ReviewerName)
                .IsRequired()
                .HasMaxLength(50);

            Property(pr => pr.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            Property(pr => pr.Comments)
                .HasMaxLength(3850);
        }
    }
}