using App.Domain.Core.BaseData._2_Dtos;
using App.Domain.Core.User.Costumer._2_Dtos;

namespace App.Domain.Core.User.Costumer._3_Contracts._3_Repositories;

public interface ICostumerCommandRepository
{
    Task AddAsync(CostumerDto costumerDto);
    Task UpdateAsync(CostumerDto costumerDto);
    Task DeleteAsync(int costumerId);
}