using AW.Core.Domain.HumanResources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class ShiftConfiguration : EntityTypeConfiguration<Shift>
    {
        public ShiftConfiguration()
        {
            ToTable("HumanResources.Shift");

            Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}