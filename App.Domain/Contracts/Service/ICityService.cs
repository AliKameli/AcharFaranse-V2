using App.Domain.Dtos;

namespace App.Domain.Contracts.Service;

public interface ICityService
{
    Task EnsureExistsByIdAsync(int cityId);
    Task EnsureExistsByNameAsync(string cityName);
    Task EnsureDoesNotExistAsync(string cityName);
    Task<int> AddAsync(CityDto cityDto);
    Task UpdateAsync(CityDto cityDto);
    Task DeleteAsync(int cityId);
    Task<List<CityDto>> GetAllAsync();
    Task<List<CityDto>> SearchByNameAsync(string cityName);
    Task<CityDto> GetByIdAsync(int cityId);
    Task<CityDto> GetByNameAsync(string cityName);
}