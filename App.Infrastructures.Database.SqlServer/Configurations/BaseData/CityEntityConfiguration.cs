using App.Domain.Core.BaseData._1_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructures.Database.SqlServer.Configurations.BaseData;

public class CityEntityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        throw new NotImplementedException();
    }
}