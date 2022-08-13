using App.Domain.Core.BaseData._2_Dtos;

namespace App.Domain.Core.BaseData._4_Contracts._3_Repositories;

public interface ICityQueryRepository
{
    Task<List<CityDto>> GetAllAsync();
    Task<CityDto?> GetByIdAsync(int cityId);
    Task<CityDto?> GetByNameAsync(string cityName);
}