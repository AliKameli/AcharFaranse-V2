using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Configurations;

public class JobCategoryWorkerConfig : IEntityTypeConfiguration<JobCategoryWorker>
{
    public void Configure(EntityTypeBuilder<JobCategoryWorker> builder)
    {
        builder.HasKey(x => new {x.JobCategoryId, x.WorkerId});

        builder.HasOne(y => y.Worker)
            .WithMany(y => y.JobCategoryWorkers)
            .HasForeignKey(y => y.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(y => y.JobCategory)
            .WithMany(y => y.JobCategoryWorkers)
            .HasForeignKey(y => y.JobCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}