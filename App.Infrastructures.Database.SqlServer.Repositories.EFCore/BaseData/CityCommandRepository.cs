using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.BaseData._4_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.BaseData
{
    public class CityCommandRepository : ICityCommandRepository
    {
        private readonly AppDbContext _context;

        public CityCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CityDto cityDto)
        {
            City record = new City
            {
                Name = cityDto.Name
            };
            await _context.Cities.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CityDto cityDto)
        {
            var record = await _context.Cities.SingleAsync(x => x.Id == cityDto.Id);
            record.Name = cityDto.Name;
            record.LastUpdateDateTime = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int cityId)
        {
            var record = await _context.Cities.SingleAsync(x => x.Id == cityId);
            record.IsDeleted = true;
            record.DeleteDateTime = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
        }
    }
}