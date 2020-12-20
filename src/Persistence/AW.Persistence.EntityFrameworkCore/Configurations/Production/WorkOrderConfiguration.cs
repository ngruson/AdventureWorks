using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.ToTable("Production.WorkOrder");

            builder.Property(wo => wo.StockedQty)
                .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(e => e.WorkOrderRouting);
        }
    }
}