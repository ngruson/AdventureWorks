using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class IllustrationConfiguration : EntityTypeConfiguration<Domain.Illustration>
    {
        public IllustrationConfiguration()
        {
            ToTable("Illustration");

            Property(i => i.Diagram)
                .HasColumnType("xml");
        }
    }
}