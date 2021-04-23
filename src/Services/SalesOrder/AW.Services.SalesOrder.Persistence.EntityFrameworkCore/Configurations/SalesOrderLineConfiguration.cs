using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesOrderLineConfiguration : IEntityTypeConfiguration<Domain.SalesOrderLine>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesOrderLine> builder)
        {
            builder.ToTable("SalesOrderLine");
            builder.HasKey(sol => sol.Id);
            builder.Property(sol => sol.Id)
                .HasColumnName("SalesOrderLineID");

            builder.Property(sol => sol.UnitPrice)
                .HasColumnType("money");

            builder.Property(sol => sol.UnitPriceDiscount)
                .HasColumnType("money");

            builder.Ignore(sol => sol.LineTotal);
        }
    }
}