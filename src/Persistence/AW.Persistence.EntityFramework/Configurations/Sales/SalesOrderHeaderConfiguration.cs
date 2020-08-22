using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesOrderHeaderConfiguration : EntityTypeConfiguration<SalesOrderHeader>
    {
        public SalesOrderHeaderConfiguration()
        {
            ToTable("Sales.SalesOrderHeader");
            HasKey(soh => soh.Id);

            Property(sp => sp.Id)
                .HasColumnName("SalesOrderID");

            Property(soh => soh.SalesOrderNumber)
                .IsRequired()
                .HasMaxLength(25)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(soh => soh.PurchaseOrderNumber)
                .HasMaxLength(25);

            Property(soh => soh.AccountNumber)
                .HasMaxLength(15);

            Property(soh => soh.CreditCardApprovalCode)
                .HasMaxLength(15)
                 .IsUnicode(false);

            Property(soh => soh.SubTotal)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(soh => soh.TaxAmt)
                 .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(soh => soh.Freight)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(soh => soh.TotalDue)
                .HasPrecision(19, 4)
                .HasColumnType("money")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(soh => soh.Comment)
                .HasMaxLength(128);
        }
    }
}