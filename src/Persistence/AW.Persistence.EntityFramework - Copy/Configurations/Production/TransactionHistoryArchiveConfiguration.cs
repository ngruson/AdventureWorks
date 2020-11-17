using AW.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class TransactionHistoryArchiveConfiguration : EntityTypeConfiguration<TransactionHistoryArchive>
    {
        public TransactionHistoryArchiveConfiguration()
        {
            ToTable("Production.TransactionHistoryArchive");
            HasKey(tha => tha.Id);

            Property(tha => tha.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(tha => tha.TransactionType)
                .IsRequired()
                .HasMaxLength(1);

            Property(tha => tha.ActualCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}