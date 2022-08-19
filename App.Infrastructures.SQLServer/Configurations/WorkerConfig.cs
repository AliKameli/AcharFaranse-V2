using App.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.SQLServer.Configurations;

public class WorkerConfig : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasIndex(x => x.NationalId)
            .IsUnique();

        builder.HasIndex(x => x.LastName)
            .IsUnique(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .HasMaxLength(50);

        builder.Property(x => x.HomeAddress)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.NationalId)
            .HasMaxLength(10)
            .HasColumnType("char(10)");

        builder.Property(x => x.PictureFilePath)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.TotalWageEarned)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.TotalCompanyProfitEarnedFromWorker)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.MoneyOwedToCompany)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.RatingByCostumers)
            .HasDefaultValue(0);

        builder.Property(x => x.RatingCount)
            .HasDefaultValue(0);

        builder.Property(x => x.IsConfirmed)
            .HasDefaultValue(false);

        builder.HasOne(x => x.UserCity)
            .WithMany(x => x.Workers)
            .HasForeignKey(x => x.UserCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.IdentityUser)
            .WithOne()
            .HasPrincipalKey<IdentityUser<int>>(x => x.Id)
            .HasForeignKey<Worker>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.WorkerJobs)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.JobCategories)
            .WithMany(x => x.Workers)
            .UsingEntity<JobCategoryWorker>();

        builder.HasMany(x => x.JobCategoryWorkers)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.JobWorkerProposals)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.JobsProposed)
            .WithMany(x => x.ProposedWorkers)
            .UsingEntity<JobWorkerProposal>();

        builder.HasMany(x => x.JobPictures)
            .WithOne(x => x.Worker)
            .HasForeignKey(x => x.WorkerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}