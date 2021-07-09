using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesPerson.Infrastructure.EFCore.Configurations
{
    public class SalesPersonConfiguration : IEntityTypeConfiguration<Core.Entities.SalesPerson>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesPerson> builder)
        {
            builder.ToTable("SalesPerson");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("SalesPersonID");
        }
    }
}