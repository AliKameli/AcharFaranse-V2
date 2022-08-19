using App.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.SQLServer.Configurations;

public class CostumerConfig : IEntityTypeConfiguration<Costumer>
{
    public void Configure(EntityTypeBuilder<Costumer> builder)
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

        builder.Property(x => x.NationalId)
            .HasMaxLength(10)
            .HasColumnType("char(10)");

        builder.Property(x => x.PictureFilePath)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.TotalMoneyPaid)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.TotalCompanyProfitEarnedFromCostumer)
            .HasPrecision(10, 1)
            .HasDefaultValue(0);

        builder.Property(x => x.RatingByWorkers)
            .HasDefaultValue(0);

        builder.Property(x => x.RatingCount)
            .HasDefaultValue(0);

        builder.Property(x => x.IsConfirmed)
            .HasDefaultValue(false);

        builder.HasOne(x => x.UserCity)
            .WithMany(x => x.Costumers)
            .HasForeignKey(x => x.UserCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.IdentityUser)
            .WithOne()
            .HasPrincipalKey<IdentityUser<int>>(x => x.Id)
            .HasForeignKey<Costumer>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CostumerAddresses)
            .WithOne(x => x.Costumer)
            .HasForeignKey(x => x.CostumerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Costumer)
            .HasForeignKey(x => x.CostumerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.CostumerJobs)
            .WithOne(x => x.Costumer)
            .HasForeignKey(x => x.CostumerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.JobPictures)
            .WithOne(x => x.Costumer)
            .HasForeignKey(x => x.CostumerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}