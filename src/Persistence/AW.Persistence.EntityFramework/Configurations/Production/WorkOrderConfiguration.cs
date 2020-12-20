using AW.Core.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class WorkOrderConfiguration : EntityTypeConfiguration<WorkOrder>
    {
        public WorkOrderConfiguration()
        {
            ToTable("Production.WorkOrder");

            Property(wo => wo.StockedQty)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(e => e.WorkOrderRouting);
        }
    }
}