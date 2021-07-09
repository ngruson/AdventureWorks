using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SalesOrderLineConfiguration : EntityTypeConfiguration<SalesOrderLine>
    {
        public SalesOrderLineConfiguration()
        {
            ToTable("SalesOrderLine");
            HasKey(p => p.Id);
            Property(sol => sol.Id)
                .HasColumnName("SalesOrderLineID");

            Property(sol => sol.UnitPrice)
                .HasColumnType("money");

            Property(sol => sol.UnitPriceDiscount)
                .HasColumnType("money");

            Ignore(sol => sol.LineTotal);
        }
    }
}