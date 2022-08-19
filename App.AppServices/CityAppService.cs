using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Repo;
using App.Domain.Dtos;
using Microsoft.Extensions.Caching.Distributed;

namespace App.AppServices;

public class CityAppService : ICityAppService
{
    private readonly IDistributedCache _cache;
    private readonly ICityRepo _cityService;

    public CityAppService(ICityRepo cityService, IDistributedCache cache)
    {
        _cityService = cityService;
        _cache = cache;
    }

    public async Task<int> AddAsync(CityDto cityDto)
    {
        var result = await _cityService.AddAsync(cityDto);
        await _cache.SetAsync("Cities", await _cityService.GetAllAsync());

        return result;
    }

    public async Task UpdateAsync(CityDto cityDto)
    {
        await _cityService.UpdateAsync(cityDto);
    }

    public async Task DeleteAsync(int cityId)
    {
        await _cityService.DeleteAsync(cityId);
    }

    public async Task<List<CityDto>> GetAllAsync()
    {
        if (_cache.TryGetValue("Cities", out List<CityDto> result))
        {
            return result;
        }

        result = await _cityService.GetAllAsync();
        await _cache.SetAsync("Cities", result);

        return result;
    }

    public async Task<List<CityDto>> SearchByNameAsync(string cityName)
    {
        return await _cityService.SearchByNameAsync(cityName);
    }

    public async Task<CityDto> GetByIdAsync(int cityId)
    {
        return await _cityService.GetByIdAsync(cityId);
    }

    public async Task<CityDto> GetByNameAsync(string cityName)
    {
        return await _cityService.GetByNameAsync(cityName);
    }
}