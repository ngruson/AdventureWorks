using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Domain.Person>
    {
        public void Configure(EntityTypeBuilder<Domain.Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(p => p.Id);
        }
    }
}