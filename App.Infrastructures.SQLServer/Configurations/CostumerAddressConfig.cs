using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.SQLServer.Configurations;

public class CostumerAddressConfig : IEntityTypeConfiguration<CostumerAddress>
{
    public void Configure(EntityTypeBuilder<CostumerAddress> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.FullAddress)
            .HasMaxLength(256);

        builder.Property(x => x.ReceivingPersonFullName)
            .HasMaxLength(50);

        builder.Property(x => x.ReceivingPersonPhoneNumber)
            .HasMaxLength(25);

        builder.Property(x => x.GpsCoordinates)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Costumer)
            .WithMany(x => x.CostumerAddresses)
            .HasForeignKey(x => x.CostumerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AddressCity)
            .WithMany(x => x.CostumerAddresses)
            .HasForeignKey(x => x.AddressCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.CostumerAddress)
            .HasForeignKey(x => x.CostumerAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}