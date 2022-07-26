using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Configurations;

public class JobCategoryConfig : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique(false);

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.PictureFilePath)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.EstimatedWageCost)
            .HasPrecision(10, 1)
            .IsRequired(false);

        builder.HasOne(x => x.ParentJobCategory)
            .WithMany(x => x.ChildrenJobCategories)
            .HasForeignKey(x => x.ParentJobCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ChildrenJobCategories)
            .WithOne(x => x.ParentJobCategory)
            .HasForeignKey(x => x.ParentJobCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.JobCategory)
            .HasForeignKey(x => x.JobCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Workers)
            .WithMany(x => x.JobCategories)
            .UsingEntity<JobCategoryWorker>();

        builder.HasMany(x => x.JobCategoryWorkers)
            .WithOne(x => x.JobCategory)
            .HasForeignKey(x => x.JobCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}