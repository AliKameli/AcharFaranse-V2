using App.Domain.Dtos;

namespace App.Domain.Contracts.AppService;

public interface ICostumerAddressAppService
{
    Task<int> AddAsync(CostumerAddressDto costumerAddressDto);
    Task UpdateAsync(CostumerAddressDto costumerAddressDto);
    Task DeleteAsync(int costumerAddressId);
    Task<List<CostumerAddressDto>> GetAllAsync();
    Task<CostumerAddressDto> GetByIdAsync(int costumerAddressId);
    Task<List<CostumerAddressDto>> GetByCostumerIdAsync(int costumerId);
}