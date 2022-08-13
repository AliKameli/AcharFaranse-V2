using App.Domain.Core.BaseData._1_Entities;
using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.BaseData._4_Contracts._3_Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.SqlServer.Repositories.EFCore.BaseData;

public class CityQueryRepository : ICityQueryRepository
{
    private readonly AppDbContext _context;

    public CityQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CityDto>> GetAllAsync()
    {
        return await _context.Cities.Select(x => new CityDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();
    }

    public async Task<CityDto?> GetByIdAsync(int cityId)
    {
        var record = await _context.Cities
            .Where(x => x.Id == cityId)
            .Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();

        return record;
    }

    public async Task<CityDto?> GetByNameAsync(string cityName)
    {
        var record = await _context.Cities
            .Where(x => x.Name == cityName)
            .Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();

        return record;
    }
}