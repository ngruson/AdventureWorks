using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<Core.Entities.SalesOrder>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");
            builder.HasKey(_ => _.Id);
            
            builder.Property(_ => _.Id)
                .HasColumnName("SalesOrderID");
            
            builder.Property(_ => _.SalesOrderNumber)
                .HasComputedColumnSql();

            builder.OwnsOne(_ => _.BillToAddress);
            builder.OwnsOne(_ => _.ShipToAddress);

            builder.Ignore(_ => _.SubTotal);
            builder.Ignore(_ => _.TaxAmt);
            builder.Ignore(_ => _.TaxRate);
            builder.Ignore(_ => _.TotalDue);
        }
    }
}