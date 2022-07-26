using App.Domain.Dtos;

namespace App.Domain.Contracts.Service;

public interface ICostumerService
{
    Task EnsureExistsByIdAsync(int costumerId);
    Task EnsureExistsByNationalIdAsync(string nationalId);
    Task EnsureDoesNotExistAsync(string nationalId);
    Task<int> AddAsync(CostumerDto costumerDto);
    Task UpdateAsync(CostumerDto costumerDto);
    Task DeleteAsync(int costumerId);
    Task<List<CostumerDto>> GetAllAsync();
    Task<CostumerDto> GetByIdAsync(int costumerId);
    Task<CostumerDto> GetByNationalIdAsync(string nationalId);
    Task<List<CostumerDto>> SearchAsync(string? name = null, string? nationalId = null);
}