using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SalesOrderLineConfiguration : EntityTypeConfiguration<Domain.SalesOrderLine>
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