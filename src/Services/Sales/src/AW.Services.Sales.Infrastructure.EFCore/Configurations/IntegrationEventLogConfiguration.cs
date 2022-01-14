using AW.SharedKernel.EventBus.IntegrationEventLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class IntegrationEventLogEntryConfiguration : IEntityTypeConfiguration<IntegrationEventLogEntry>
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");
            builder.HasKey(p => p.EventId);
            builder.Property(s => s.EventId)
                .HasColumnName("EventId");
        }
    }
}