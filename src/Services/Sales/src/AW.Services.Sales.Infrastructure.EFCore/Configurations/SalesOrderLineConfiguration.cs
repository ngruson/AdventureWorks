using AW.Services.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesOrderLineConfiguration : IEntityTypeConfiguration<SalesOrderLine>
    {
        public void Configure(EntityTypeBuilder<SalesOrderLine> builder)
        {
            builder.ToTable("SalesOrderLine");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .HasColumnName("SalesOrderLineID");

            builder.Property(_ => _.UnitPrice)
                .HasColumnType("money");

            builder.Property(_ => _.UnitPriceDiscount)
                .HasColumnType("money");

            builder.Ignore(_ => _.LineTotal);
        }
    }
}