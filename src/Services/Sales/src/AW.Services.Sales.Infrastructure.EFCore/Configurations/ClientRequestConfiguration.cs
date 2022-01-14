using AW.Services.Sales.Core.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Sales.Infrastructure.EFCore.Configurations
{
    public class ClientRequestConfiguration : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> builder)
        {
            builder.ToTable("ClientRequest");
            builder.HasKey(p => p.Id);
            builder.Property(s => s.Id)
                .HasColumnName("ClientRequestID");
        }
    }
}