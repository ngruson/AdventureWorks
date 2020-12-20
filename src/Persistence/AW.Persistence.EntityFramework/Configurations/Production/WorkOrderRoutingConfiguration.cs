using AW.Core.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class WorkOrderRoutingConfiguration : EntityTypeConfiguration<WorkOrderRouting>
    {
        public WorkOrderRoutingConfiguration()
        {
            ToTable("Production.WorkOrderRouting");
            HasKey(wor => new { wor.Id, wor.ProductID, wor.OperationSequence });

            Property(wor => wor.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(wor => wor.ProductID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(wor => wor.OperationSequence)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(wor => wor.ActualResourceHrs)
                .HasPrecision(9, 4);

            Property(wor => wor.PlannedCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(wor => wor.ActualCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}