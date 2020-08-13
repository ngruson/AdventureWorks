using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            ToTable("Sales.Store");
            HasKey(s => s.BusinessEntityID);

            Property(s => s.BusinessEntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(s => s.Name)
                .HasMaxLength(50);

            Property(s => s.Demographics)
                .HasColumnType("xml");
        }
    }
}