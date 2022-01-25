using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<Core.Entities.SalesOrder>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id)
                .HasColumnName("SalesOrderID");
            
            builder.Property(p => p.SalesOrderNumber)
                .HasComputedColumnSql();

            //builder.Property(_ => _.CreditCardID)
            //    .HasColumnName("CreditCardID");

            builder.Ignore(p => p.SubTotal);
            builder.Ignore(p => p.TaxAmt);
            builder.Ignore(p => p.TaxRate);
            builder.Ignore(p => p.Freight);
            builder.Ignore(p => p.TotalDue);
        }
    }
}