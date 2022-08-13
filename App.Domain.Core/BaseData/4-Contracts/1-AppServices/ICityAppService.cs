using App.Domain.Core.BaseData._2_Dtos;

namespace App.Domain.Core.BaseData._4_Contracts._1_AppServices;

public interface ICityAppService
{
    Task<List<CityDto>> GetAllAsync();
    Task SetAsync(CityDto cityDto);
    Task<CityDto> GetAsync(int cityId);
    Task<CityDto> GetAsync(string cityName);
    Task UpdateAsync(CityDto cityDto);
    Task DeleteAsync(int cityId);
}