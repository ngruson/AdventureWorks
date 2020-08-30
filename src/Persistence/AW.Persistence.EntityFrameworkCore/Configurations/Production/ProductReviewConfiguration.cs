using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("Production.ProductReview");

            builder.Property(pr => pr.ReviewerName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pr => pr.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pr => pr.Comments)
                .HasMaxLength(3850);
        }
    }
}