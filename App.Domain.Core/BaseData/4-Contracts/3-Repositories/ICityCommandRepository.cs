using App.Domain.Core.BaseData._2_Dtos;

namespace App.Domain.Core.BaseData._4_Contracts._3_Repositories
{
    public interface ICityCommandRepository
    {
        Task AddAsync(CityDto cityDto);
        Task UpdateAsync(CityDto cityDto);
        Task DeleteAsync(int cityId);
    }
}