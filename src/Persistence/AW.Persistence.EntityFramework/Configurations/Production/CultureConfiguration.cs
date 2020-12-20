using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class CultureConfiguration : EntityTypeConfiguration<Culture>
    {
        public CultureConfiguration()
        {
            ToTable("Production.Culture");

            Property(c => c.Id);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}