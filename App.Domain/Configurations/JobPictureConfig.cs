using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Configurations;

public class JobPictureConfig : IEntityTypeConfiguration<JobPicture>
{
    public void Configure(EntityTypeBuilder<JobPicture> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileSavePath)
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.IsConfirmed)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.JobPictures)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Worker)
            .WithMany(x => x.JobPictures)
            .HasForeignKey(x => x.WorkerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Costumer)
            .WithMany(x => x.JobPictures)
            .HasForeignKey(x => x.CostumerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}