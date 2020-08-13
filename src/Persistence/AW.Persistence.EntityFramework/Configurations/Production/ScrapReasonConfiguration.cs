using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ScrapReasonConfiguration : EntityTypeConfiguration<ScrapReason>
    {
        public ScrapReasonConfiguration()
        {
            ToTable("Production.ScrapReason");

            Property(sr => sr.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}