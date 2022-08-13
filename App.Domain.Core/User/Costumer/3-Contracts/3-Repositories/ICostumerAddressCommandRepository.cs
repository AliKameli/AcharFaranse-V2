using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.Job._2_Dtos;
using App.Domain.Core.User.Costumer._2_Dtos;

namespace App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;

public interface ICostumerAddressCommandRepository
{
    Task AddAsync(CostumerAddressDto costumerAddressDto);
    Task UpdateAsync(CostumerAddressDto costumerAddressDto);
    Task DeleteAsync(int costumerAddressId);
}