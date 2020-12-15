using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            builder.ToTable("SalesOrderHeader", "Sales");
            builder.HasKey(soh => soh.Id);

            builder.Property(sp => sp.Id)
                .HasColumnName("SalesOrderID");

            builder.Property(soh => soh.SalesOrderNumber)
                .IsRequired()
                .HasMaxLength(25)
                .ValueGeneratedOnAdd();

            builder.Property(soh => soh.PurchaseOrderNumber)
                .HasMaxLength(25);

            builder.Property(soh => soh.AccountNumber)
                .HasMaxLength(15);

            builder.Property(soh => soh.CreditCardApprovalCode)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(soh => soh.SubTotal)
                .HasColumnType("money");

            builder.Property(soh => soh.TaxAmt)
                .HasColumnType("money");

            builder.Property(soh => soh.Freight)
                .HasColumnType("money");

            builder.Property(soh => soh.TotalDue)
                .HasColumnType("money")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(soh => soh.Comment)
                .HasMaxLength(128);
        }
    }
}