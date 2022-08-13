using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Costumer._2_Dtos;

namespace App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;

public interface ICostumerAddressQueryRepository
{
    Task<List<CostumerAddressDto>> GetAllAsync();
    Task<CostumerAddressDto?> GetByIdAsync(int costumerAddressId);
    Task<List<CostumerAddressDto>?> GetAllByCostumerIdAsync(int costumerId);
}