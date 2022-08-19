using App.Domain.Entities;
using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.SQLServer.Configurations;

public class JobConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.OnlinePaymentReceiptInfo)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.CostumerEstimatedFinalCost)
            .HasPrecision(10, 1)
            .IsRequired(false);

        builder.Property(x => x.WageCost)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.MaterialCost)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.CompanyProfit)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.FinalCost)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.IsClosed)
            .HasDefaultValue(false);

        builder.Property(x => x.IsOnlinePayment)
            .HasDefaultValue(false);

        builder.Property(x => x.IsPictureAttached)
            .HasDefaultValue(false);

        builder.Property(x => x.JobStatus)
            .HasDefaultValue(JobStatusEnum.RequestedByCostumer);

        builder.HasOne(x => x.Costumer)
            .WithMany(x => x.CostumerJobs)
            .HasForeignKey(x => x.CostumerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.JobCity)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.JobCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.JobCategory)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.JobCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CostumerAddress)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.CostumerAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Worker)
            .WithMany(x => x.WorkerJobs)
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.JobPictures)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.JobWorkerProposals)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProposedWorkers)
            .WithMany(x => x.JobsProposed)
            .UsingEntity<JobWorkerProposal>();
    }
}