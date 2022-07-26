using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Configurations;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.JobCity)
            .HasForeignKey(x => x.JobCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Workers)
            .WithOne(x => x.UserCity)
            .HasForeignKey(x => x.UserCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Costumers)
            .WithOne(x => x.UserCity)
            .HasForeignKey(x => x.UserCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.CostumerAddresses)
            .WithOne(x => x.AddressCity)
            .HasForeignKey(x => x.AddressCityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}