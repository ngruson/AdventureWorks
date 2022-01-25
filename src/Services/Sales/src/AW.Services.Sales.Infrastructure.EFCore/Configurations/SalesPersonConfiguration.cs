using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesPersonConfiguration : IEntityTypeConfiguration<Core.Entities.SalesPerson>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesPerson> builder)
        {
            builder.ToTable("SalesPerson");

            builder.Property(c => c.Id)
                .HasColumnName("PersonID");
        }
    }
}