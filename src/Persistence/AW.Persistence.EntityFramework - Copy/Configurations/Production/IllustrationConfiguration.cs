using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class IllustrationConfiguration : EntityTypeConfiguration<Illustration>
    {
        public IllustrationConfiguration()
        {
            ToTable("Production.Illustration");

            Property(i => i.Diagram)
                .HasColumnType("xml");
        }
    }
}