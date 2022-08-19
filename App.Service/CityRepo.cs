using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using App.Domain.Entities;
using App.Infrastructures.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Repo;

public class CityRepo : ICityRepo
{
    private readonly AppDbContext _dbContext;

    public CityRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureExistsByIdAsync(int cityId)
    {
        var result = await _dbContext.Cities.AnyAsync(x => x.Id == cityId);

        if (!result) throw new Exception($"شهر با شناسه {cityId} وجود ندارد !");
    }

    public async Task EnsureExistsByNameAsync(string cityName)
    {
        var result = await _dbContext.Cities.AnyAsync(x => x.Name == cityName);

        if (!result) throw new Exception($"شهر {cityName} وجود ندارد !");
    }

    public async Task EnsureDoesNotExistAsync(string cityName)
    {
        var result = await _dbContext.Cities.AnyAsync(x => x.Name == cityName);

        if (result) throw new Exception($"شهر {cityName} وجود دارد !");
    }

    public async Task<int> AddAsync(CityDto cityDto)
    {
        await EnsureDoesNotExistAsync(cityDto.Name);

        var record = new City
        {
            Name = cityDto.Name
        };

        var result = await _dbContext.Cities.AddAsync(record);

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task UpdateAsync(CityDto cityDto)
    {
        await EnsureExistsByIdAsync(cityDto.Id);

        if (await _dbContext.Cities.AnyAsync(x =>
                x.Name == cityDto.Name &&
                x.Id != cityDto.Id))
            throw new Exception($"شهر {cityDto.Name} وجود دارد !");

        var record = await _dbContext.Cities.FirstAsync(x => x.Id == cityDto.Id);

        record.Name = cityDto.Name;

        record.LastUpdateDateTime = DateTimeOffset.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int cityId)
    {
        await EnsureExistsByIdAsync(cityId);

        var record = await _dbContext.Cities.FirstAsync(x => x.Id == cityId);

        try
        {
            _dbContext.Cities.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(innerException: e, message: $"شهر {record.Name} در حال استفاده است و قابل حذف نیست !");
        }
    }

    public async Task<List<CityDto>> GetAllAsync()
    {
        var records = await _dbContext.Cities.Select(x => new CityDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name
            })
            .ToListAsync();

        return records;
    }

    public async Task<List<CityDto>> SearchByNameAsync(string cityName)
    {
        var records = await _dbContext.Cities
            .Where(x => x.Name.Contains(cityName))
            .Select(x => new CityDto
            {
                Id = x.Id,
                CreationDateTime = x.CreationDateTime,
                LastUpdateDateTime = x.LastUpdateDateTime,
                Name = x.Name
            })
            .ToListAsync();

        return records;
    }

    public async Task<CityDto> GetByIdAsync(int cityId)
    {
        await EnsureExistsByIdAsync(cityId);

        var record = await _dbContext.Cities.AsNoTracking().FirstAsync(x => x.Id == cityId);

        var result = new CityDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Name = record.Name
        };

        return result;
    }

    public async Task<CityDto> GetByNameAsync(string cityName)
    {
        await EnsureExistsByNameAsync(cityName);

        var record = await _dbContext.Cities.AsNoTracking().FirstAsync(x => x.Name == cityName);

        var result = new CityDto
        {
            Id = record.Id,
            CreationDateTime = record.CreationDateTime,
            LastUpdateDateTime = record.LastUpdateDateTime,
            Name = record.Name
        };

        return result;
    }
}