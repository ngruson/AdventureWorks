using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> builder)
        {
            builder.ToTable("Person.BusinessEntity");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("BusinessEntityID");
        }
    }
}