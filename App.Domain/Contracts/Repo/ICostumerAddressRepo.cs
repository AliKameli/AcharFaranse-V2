using App.Domain.Dtos;

namespace App.Domain.Contracts.Repo;

public interface ICostumerAddressRepo
{
    Task EnsureExistsByIdAsync(int costumerAddressId);
    Task<bool> InUseStatus(int costumerAddressId);
    Task<int> AddAsync(CostumerAddressDto costumerAddressDto);
    Task UpdateAsync(CostumerAddressDto costumerAddressDto);
    Task DeleteAsync(int costumerAddressId);
    Task SoftDeleteAsync(int costumerAddressId);
    Task<List<CostumerAddressDto>> GetAllAsync();
    Task<CostumerAddressDto> GetByIdAsync(int costumerAddressId);
    Task<List<CostumerAddressDto>> GetByCostumerIdAsync(int costumerId);
}