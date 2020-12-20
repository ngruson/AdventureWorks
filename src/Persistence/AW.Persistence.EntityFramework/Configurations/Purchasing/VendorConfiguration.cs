using AW.Core.Domain.Purchasing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Purchasing
{
    public class VendorConfiguration : EntityTypeConfiguration<Vendor>
    {
        public VendorConfiguration()
        {
            ToTable("Purchasing.Vendor");
            HasKey(v => v.Id);

            Property(v => v.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(v => v.AccountNumber)
                .IsRequired()
                .HasMaxLength(15);

            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(v => v.PurchasingWebServiceURL)
                .HasMaxLength(1024);
        }
    }
}