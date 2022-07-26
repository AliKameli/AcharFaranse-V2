using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface ICityAppService
{
    Task<int> AddAsync(CityDto cityDto);
    Task UpdateAsync(CityDto cityDto);
    Task DeleteAsync(int cityId);
    Task<List<CityDto>> GetAllAsync();
    Task<List<CityDto>> SearchByNameAsync(string cityName);
    Task<CityDto> GetByIdAsync(int cityId);
    Task<CityDto> GetByNameAsync(string cityName);
}