using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class WorkOrderRoutingConfiguration : IEntityTypeConfiguration<WorkOrderRouting>
    {
        public void Configure(EntityTypeBuilder<WorkOrderRouting> builder)
        {
            builder.ToTable("Production.WorkOrderRouting");
            builder.HasKey(wor => new { wor.Id, wor.ProductID, wor.OperationSequence });

            builder.Property(wor => wor.Id)
                .ValueGeneratedNever();

            builder.Property(wor => wor.ProductID)
                .ValueGeneratedNever();

            builder.Property(wor => wor.OperationSequence)
                .ValueGeneratedNever();

            builder.Property(wor => wor.ActualResourceHrs)
                .HasColumnType("decimal(9,4)");

            builder.Property(wor => wor.PlannedCost)
                .HasColumnType("money");

            builder.Property(wor => wor.ActualCost)
                .HasColumnType("money");
        }
    }
}