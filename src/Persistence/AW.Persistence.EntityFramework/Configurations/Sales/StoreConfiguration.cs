using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            ToTable("Sales.Store");
            HasKey(s => s.Id);

            Property(s => s.Id)
                .HasColumnName("BusinessEntityID");

            Property(s => s.Name)
                .HasMaxLength(50);

            Property(s => s.Demographics)
                .HasColumnType("xml");

            Property(s => s.SalesPersonID)
                .HasColumnName("SalesPersonID");
        }
    }
}