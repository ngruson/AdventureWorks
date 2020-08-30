using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.ToTable("Production.TransactionHistory");
            builder.HasKey(th => th.Id);

            builder.Property(th => th.TransactionType)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(th => th.ActualCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}