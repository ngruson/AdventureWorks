using AW.Domain.HumanResources;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class JobCandidateConfiguration : EntityTypeConfiguration<JobCandidate>
    {
        public JobCandidateConfiguration()
        {
            ToTable("HumanResources.JobCandidate");

            Property(jc => jc.Resume)
                .HasColumnType("xml");
        }
    }
}