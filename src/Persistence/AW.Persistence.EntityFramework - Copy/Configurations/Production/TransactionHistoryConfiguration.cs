using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class TransactionHistoryConfiguration : EntityTypeConfiguration<TransactionHistory>
    {
        public TransactionHistoryConfiguration()
        {
            ToTable("Production.TransactionHistory");
            HasKey(th => th.Id);

            Property(th => th.TransactionType)
                .IsRequired()
                .HasMaxLength(1);

            Property(th => th.ActualCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}