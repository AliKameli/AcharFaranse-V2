using App.Domain.Contracts.AppService;
using App.Domain.Contracts.Service;
using App.Domain.Dtos;

namespace App.AppServices;

public class CityAppService : ICityAppService
{
    private readonly ICityService _cityService;

    public CityAppService(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<int> AddAsync(CityDto cityDto)
    {
        return await _cityService.AddAsync(cityDto);
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
        return await _cityService.GetAllAsync();
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