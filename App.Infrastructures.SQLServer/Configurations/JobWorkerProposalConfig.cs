using App.Domain.Entities;
using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.SQLServer.Configurations;

public class JobWorkerProposalConfig : IEntityTypeConfiguration<JobWorkerProposal>
{
    public void Configure(EntityTypeBuilder<JobWorkerProposal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new {x.JobId, x.WorkerId});

        builder.Property(x => x.ProposedPrice)
            .HasPrecision(10, 1);

        builder.Property(x => x.ProposalStatus)
            .HasDefaultValue(ProposalStatusEnum.ProposedByWorker);

        builder.Property(x => x.WorkerComment)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.HasOne(x => x.Worker)
            .WithMany(x => x.JobWorkerProposals)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.JobWorkerProposals)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}