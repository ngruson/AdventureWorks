using AW.Core.Domain.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.HumanResources
{
    public class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate>
    {
        public void Configure(EntityTypeBuilder<JobCandidate> builder)
        {
            builder.ToTable("HumanResources.JobCandidate");

            builder.Property(jc => jc.Resume)
                .HasColumnType("xml");
        }
    }
}