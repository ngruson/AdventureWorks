using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class TransactionHistoryArchiveConfiguration : IEntityTypeConfiguration<TransactionHistoryArchive>
    {
        public void Configure(EntityTypeBuilder<TransactionHistoryArchive> builder)
        {
            builder.ToTable("Production.TransactionHistoryArchive");
            builder.HasKey(tha => tha.Id);

            builder.Property(tha => tha.Id)
                .ValueGeneratedNever();

            builder.Property(tha => tha.TransactionType)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(tha => tha.ActualCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}